using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using BVSeoSdkDotNet.BVException;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Util;
using log4net;

namespace BVSeoSdkDotNet.Content.Loaders
{
    /// <summary>
    ///     Implementation of <see cref="IContentLoader" /> that loads content from a URL.
    /// </summary>
    internal class HttpContentLoader : IContentLoader
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     Time, in milliseconds, to use for <see cref="HttpWebRequest.Timeout" /> on
        ///     <see cref="HttpWebRequest">HttpWebRequests</see>
        /// </summary>
        protected readonly int ConnectionTimeout;

        /// <summary>
        ///     Encoding to use when reading <see cref="HttpWebResponse" /> content from a stream.
        /// </summary>
        protected readonly Encoding Encoding;

        /// <summary>
        ///     Host to use for <see cref="HttpWebRequest.Proxy" />
        /// </summary>
        protected readonly string ProxyHost;

        /// <summary>
        ///     Port to use for <see cref="HttpWebRequest.Proxy" />
        /// </summary>
        protected readonly int ProxyPort;

        /// <summary>
        ///     Time to wait, in milliseconds to wait when writing to or reading from <see cref="HttpWebRequest" /> or
        ///     <see cref="HttpWebResponse" /> streams.
        /// </summary>
        protected readonly int SocketTimeout;

        /// <summary>
        ///     Browser and system details to be forwarded as the <see cref="HttpRequestHeader.UserAgent" /> when loading content
        ///     via HTTP.
        /// </summary>
        protected readonly string UserAgent;

        /// <summary>
        ///     Configurable ctor for <see cref="HttpContentLoader" />.
        /// </summary>
        /// <param name="config">
        ///     <list type="bullet">
        ///         <listheader>
        ///             <description>Supported <see cref="BVClientConfig" /> properties</description>
        ///         </listheader>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.CONNECT_TIMEOUT" />
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.SOCKET_TIMEOUT" />
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.PROXY_PORT" />
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.PROXY_HOST" />
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.CHARSET" />
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="userAgent">
        ///     Browser and system details to be forwarded as the <see cref="HttpRequestHeader.UserAgent" />
        ///     when loading content via HTTP.
        /// </param>
        public HttpContentLoader(BVConfiguration config, string userAgent)
        {
            ConnectionTimeout = int.Parse(config.getProperty(BVClientConfig.CONNECT_TIMEOUT));
            SocketTimeout = int.Parse(config.getProperty(BVClientConfig.SOCKET_TIMEOUT));
            ProxyPort = int.Parse(config.getProperty(BVClientConfig.PROXY_PORT));
            ProxyHost = config.getProperty(BVClientConfig.PROXY_HOST);
            Encoding = EncodingParser.GetEncoding(config.getProperty(BVClientConfig.CHARSET));
            UserAgent = userAgent;
        }

        public string LoadContent(Uri uri)
        {
            string content = null;
            try
            {
                var httpRequest = (HttpWebRequest) WebRequest.Create(uri);
                httpRequest.AutomaticDecompression = DecompressionMethods.GZip;
                httpRequest.Timeout = ConnectionTimeout;
                httpRequest.ReadWriteTimeout = SocketTimeout;
                httpRequest.UserAgent = UserAgent;
                if (!string.IsNullOrEmpty(ProxyHost) &&
                    !ProxyHost.Equals("none", StringComparison.InvariantCultureIgnoreCase))
                {
                    var proxy = new WebProxy(ProxyHost, ProxyPort);
                    httpRequest.Proxy = proxy;
                }

                var webResponse = (HttpWebResponse) httpRequest.GetResponse();
                using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding))
                {
                    content = reader.ReadToEnd();
                }
            }
            catch (ProtocolViolationException e)
            {
                Logger.Error(BVMessageUtil.getMessage("ERR0012"), e);
                throw new BVSdkException("ERR0012");
            }
            catch (IOException e)
            {
                Logger.Error(BVMessageUtil.getMessage("ERR0019"), e);
                throw new BVSdkException("ERR0019");
            }
            catch (WebException e)
            {
                Logger.Error(BVMessageUtil.getMessage("ERR0012"), e);
                throw new BVSdkException("ERR0012");
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new BVSdkException(e.Message);
            }

            var isValidContent = BVUtility.validateBVContent(content);
            if (!isValidContent)
            {
                Logger.Error(BVMessageUtil.getMessage("ERR0025"));
                throw new BVSdkException("ERR0025");
            }


            return content;
        }
    }
}