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
    internal class HttpContentLoader : IContentLoader
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected static Encoding DefaultEncoding;

        protected readonly int ConnectionTimeout;
        protected readonly Encoding Encoding;
        protected readonly string ProxyHost;
        protected readonly int ProxyPort;
        protected readonly int SocketTimeout;
        protected readonly string UserAgent;

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