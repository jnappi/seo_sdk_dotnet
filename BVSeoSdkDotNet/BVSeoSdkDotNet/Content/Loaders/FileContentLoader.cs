using System;
using System.IO;
using System.Reflection;
using System.Text;
using BVSeoSdkDotNet.BVException;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Util;
using log4net;

namespace BVSeoSdkDotNet.Content.Loaders
{
    /// <summary>
    ///     Implementation of <see cref="IContentLoader" /> that loads content from a file.
    /// </summary>
    internal class FileContentLoader : IContentLoader
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     Encoding to use when reading file content from a stream.
        /// </summary>
        protected readonly Encoding Encoding;

        /// <summary>
        ///     Configurable ctor for <see cref="FileContentLoader" />.
        /// </summary>
        /// <param name="config">
        ///     <list type="bullet">
        ///         <listheader>
        ///             <description>Supported <see cref="BVClientConfig" /> properties</description>
        ///         </listheader>
        ///         <item>
        ///             <description>
        ///                 <see cref="BVClientConfig.CHARSET" />
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        public FileContentLoader(BVConfiguration config)
        {
            Encoding = EncodingParser.GetEncoding(config.getProperty(BVClientConfig.CHARSET));
        }

        public string LoadContent(Uri uri)
        {
            if (File.Exists(uri.AbsolutePath))
            {
                try
                {
                    return File.ReadAllText(uri.AbsolutePath, Encoding);
                }
                catch (IOException e)
                {
                    Logger.Error(BVMessageUtil.getMessage("ERR0012"), e);
                    throw new BVSdkException("ERR0012");
                }
                catch (BVSdkException e)
                {
                    Logger.Error(e.getMessage(), e);
                    throw;
                }
                catch (Exception e)
                {
                    Logger.Error(e.Message, e);
                    throw new BVSdkException(e.Message);
                }
            }
            Logger.Error(BVMessageUtil.getMessage("ERR0012"));
            throw new BVSdkException("ERR0012");
        }
    }
}