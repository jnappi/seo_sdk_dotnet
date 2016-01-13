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
    internal class FileContentLoader : IContentLoader
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected readonly Encoding Encoding;

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