using System;
using System.Reflection;
using System.Text;
using BVSeoSdkDotNet.Util;
using log4net;
using log4net.Repository.Hierarchy;

namespace BVSeoSdkDotNet.Content.Loaders
{
    internal class EncodingParser
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        ///     Returns an encoding associated with the specified code page name.
        /// </summary>
        /// <remarks>Defaults to <see cref="Encoding.UTF8"/></remarks>
        /// <param name="name">
        ///     The code page name of the preferred encoding. Any value returned by
        ///     <see cref="P:System.Text.Encoding.WebName" /> is a valid input.
        /// </param>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="name" /> is not a valid code page name.-or- The code page
        ///     indicated by <paramref name="name" /> is not supported by the underlying platform.
        /// </exception>
        /// <filterpriority>1</filterpriority>
        /// <returns>
        ///     The <see cref="T:System.Text.Encoding" /> associated with the specified code page.
        /// </returns>
        public static Encoding GetEncoding(string name)
        {
            var encoding = Encoding.UTF8;
            try
            {
                encoding = string.IsNullOrEmpty(name) ? Encoding.UTF8 : Encoding.GetEncoding(name);
            }
            catch (Exception e)
            {
                Logger.Error(BVMessageUtil.getMessage("ERR0024"), e);
            }
            return encoding;
        }
    }
}