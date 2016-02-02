/*
 * ===========================================================================
 * Copyright 2014 Bazaarvoice, Inc.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ===========================================================================
 * 
 */

using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using BVSeoSdkDotNet.BVException;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content.Loaders;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Url;
using BVSeoSdkDotNet.Util;
using log4net;

namespace BVSeoSdkDotNet.Content
{
    /// <summary>
    ///     Implementation class for BVUIContentService
    /// </summary>
    public class BVUIContentServiceProvider : BVUIContentService
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly BVConfiguration _bvConfiguration;
        private readonly StringBuilder _message;
        private readonly StringBuilder _uiContent;

        /// <summary>
        ///     Backing field for <see cref="get_AssemblyVersion" />
        /// </summary>
        private Version _assemblyVersion;

        private BVParameters _bvParameters;
        private BVSeoSdkUrl _bvSeoSdkUrl;
        private bool _sdkEnabled;

        /// <summary>
        ///     Default Constructor to set the BVConfiguration values
        /// </summary>
        /// <param name="bvConfiguration"></param>
        public BVUIContentServiceProvider(BVConfiguration bvConfiguration)
        {
            _bvConfiguration = bvConfiguration;

            _message = new StringBuilder();
            _uiContent = new StringBuilder();
        }

        /// <summary>
        ///     Returns true if this <see cref="BVUIContentServiceProvider" /> is configured to
        ///     <see cref="BVClientConfig.LOAD_SEO_FILES_LOCALLY">load content locally.</see>
        /// </summary>
        private bool IsContentFromFile
        {
            get { return bool.Parse(_bvConfiguration.getProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY)); }
        }

        /// <summary>
        ///     Shorthand to get the Seo Content Uri from <see cref="_bvSeoSdkUrl" />.
        /// </summary>
        private Uri SeoContentUrl
        {
            get { return _bvSeoSdkUrl.seoContentUri(); }
        }

        /// <summary>
        ///     Shorthand to get the Corrected Base Uri from <see cref="_bvSeoSdkUrl" />
        /// </summary>
        private string CorrectedBaseUri
        {
            get { return _bvSeoSdkUrl.correctedBaseUri(); }
        }

        /// <summary>
        ///     The version of the <see cref="Assembly" /> in which this class is specified.
        /// </summary>
        private Version AssemblyVersion
        {
            get
            {
                if (_assemblyVersion == null)
                {
                    var assembly = Assembly.GetAssembly(GetType());
                    _assemblyVersion = assembly.GetName().Version;
                }
                return _assemblyVersion;
            }
        }

        /// <summary>
        ///     <see cref="HttpRequestHeader.UserAgent" /> value to use when making requests.
        /// </summary>
        private string UserAgent
        {
            get
            {
                var userAgent = RequestUserAgent ?? ParameterUserAgent;
                if (string.IsNullOrEmpty(userAgent))
                {
                    Logger.Warn(BVMessageUtil.getMessage("WRN0001"));}
                return string.Format("CLR/{0};bv_dotnet_sdk/{1};{2}", Environment.Version,
                    AssemblyVersion.ToString(3), userAgent);
            }
        }

        /// <summary>s
        ///     The UserAgent as specified by <see cref="_bvParameters" /> or <c>null</c>.
        /// </summary>
        private string ParameterUserAgent
        {
            get { return !string.IsNullOrEmpty(_bvParameters.UserAgent) ? _bvParameters.UserAgent : null; }
        }

        /// <summary>
        ///     The UserAgent as specified by the current <see cref="HttpRequest" /> or <c>null</c> if there is no
        ///     <see cref="HttpContext.Current">current context</see>
        ///     or if the context cannot be accessed from this <see cref="Thread" />.
        /// </summary>
        private string RequestUserAgent
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.Request.UserAgent;
                }
                Logger.Debug(BVMessageUtil.getMessage("MSG0006"));
                return null;
            }
        }

        /// <summary>
        ///     Returns true iff the
        ///     <see cref="BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE">integration code should be included</see> in the
        ///     content provided.
        /// </summary>
        private bool IncludeIntegrationCode
        {
            get { return bool.Parse(_bvConfiguration.getProperty(BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE)); }
        }

        /// <summary>
        ///     Gets a boolean value whether to Show UserAgent SEO Content
        /// </summary>
        /// <returns>A boolean value</returns>
        [Obsolete("Should always return true.")]
        public bool showUserAgentSEOContent()
        {
            return true;
        }

        /// <summary>
        ///     Sets the BV Parameters for retreiving the SEO content
        /// </summary>
        /// <param name="bvParameters">Parameters that decide the SEO Content</param>
        public void setBVParameters(BVParameters bvParameters)
        {
            _bvParameters = bvParameters;
        }

        /// <summary>
        ///     Set the URL for the SEO SDK.
        /// </summary>
        /// <param name="bvSeoSdkUrl"></param>
        public void setBVSeoSdkUrl(BVSeoSdkUrl bvSeoSdkUrl)
        {
            _bvSeoSdkUrl = bvSeoSdkUrl;
        }

        /// <summary>
        ///     Implementation to check if sdk is enabled/disabled.
        ///     The settings are based on the configurations from BVConfiguration and BVParameters.
        /// </summary>
        /// <returns>A Boolean value, true if sdk is enabled and false if it is not enabled</returns>
        public bool isSdkEnabled()
        {
            _sdkEnabled = bool.Parse(_bvConfiguration.getProperty(BVClientConfig.SEO_SDK_ENABLED));
            _sdkEnabled = _sdkEnabled || BVUtility.isRevealDebugEnabled(_bvParameters);
            return _sdkEnabled;
        }

        /// <summary>
        ///     Executes the server side call or the file level Call within a specified execution timeout.
        ///     when reload is set true then it gives from the cache that was already executed in the previous call.
        /// </summary>
        /// <param name="reload">A Boolean value to determine whether to reload from cache</param>
        /// <returns>A StringBuilder object representing the Content</returns>
        public StringBuilder executeCall(bool reload)
        {
            if (reload)
            {
                return new StringBuilder(_uiContent.ToString());
            }

            /**
             * StringBuilder depends on Length to reference the position of the next character;
             * We can effectively clear the StringBuilder by resetting it's length to '0'.
             */
            _uiContent.Length = 0;

            var isWebCrawler = false;
            var crawlerAgentPattern = _bvConfiguration.getProperty(BVClientConfig.CRAWLER_AGENT_PATTERN);
            var userAgent = UserAgent;
            if (!string.IsNullOrEmpty(crawlerAgentPattern) && _bvParameters != null &&
                !string.IsNullOrEmpty(userAgent))
            {
                crawlerAgentPattern = ".*(" + crawlerAgentPattern + ").*";
                var pattern = new Regex(crawlerAgentPattern, RegexOptions.IgnoreCase);
                isWebCrawler = pattern.IsMatch(userAgent);
            }

            var executionTimeout = isWebCrawler
                ? long.Parse(_bvConfiguration.getProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT))
                : long.Parse(_bvConfiguration.getProperty(BVClientConfig.EXECUTION_TIMEOUT));

            if (!isWebCrawler && executionTimeout <= 0)
            {
                _message.Append(BVMessageUtil.getMessage("MSG0004"));
                return new StringBuilder();
            }

            if (isWebCrawler && executionTimeout < 100)
            {
                executionTimeout = 100;
                _message.Append(BVMessageUtil.getMessage("MSG0005"));
            }

            try
            {
                var fCallFinished = false;

                if (executionTimeout > 0)
                {
                    var contentLoader = IsContentFromFile
                        ? new FileContentLoader(_bvConfiguration)
                        : (IContentLoader) new HttpContentLoader(_bvConfiguration, UserAgent);
                    fCallFinished = RunWithTimeout(Call, TimeSpan.FromMilliseconds(executionTimeout), contentLoader);
                }

                if (!fCallFinished)
                    _message.Append(string.Format(BVMessageUtil.getMessage("ERR0018"), executionTimeout));
            }
            catch (ThreadInterruptedException e)
            {
                Logger.Error(e.Message, e);
                _message.Append(e.Message);
            }
            catch (ExecutionEngineException e)
            {
                Logger.Error(e.Message, e);
                _message.Append(e.Message);
            }
            catch (TimeoutException e)
            {
                var errorCode = isWebCrawler ? "ERR0026" : "ERR0018";
                Logger.Error(string.Format(BVMessageUtil.getMessage(errorCode), executionTimeout), e);
                _message.AppendFormat(BVMessageUtil.getMessage(errorCode), executionTimeout);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new BVSdkException(e.Message);
            }

            return new StringBuilder(_uiContent.ToString());
        }

        /// <summary>
        ///     Gets the messages if there are any after executeCall is invoked or if it is still in the cache.
        /// </summary>
        /// <returns>A StringBuilder object containing the messages if any</returns>
        public StringBuilder getMessage()
        {
            return _message;
        }

        /// <summary>
        ///     Append all content to _uiContent, including Integration Code iff it should be included.
        /// </summary>
        /// <param name="contentLoader"></param>
        private void AppendContent(IContentLoader contentLoader)
        {
            if (IncludeIntegrationCode)
            {
                _uiContent.Append(GetIntegrationCode());
            }
            _uiContent.Append(contentLoader.LoadContent(SeoContentUrl));
            BVUtility.replacePageURIFromContent(_uiContent, CorrectedBaseUri);
        }

        /// <summary>
        ///     Get the Integration Code from <see cref="_bvParameters" />.
        /// </summary>
        /// <returns></returns>
        private string GetIntegrationCode()
        {
            object[] parameters = {_bvParameters.SubjectType.uriValue(), _bvParameters.SubjectId};
            var integrationScriptValue =
                _bvConfiguration.getProperty(_bvParameters.ContentType.getIntegrationScriptProperty());
            return string.Format(integrationScriptValue, parameters);
        }

        /// <summary>
        ///     Check arguments and call in to <see cref="AppendContent" />.
        /// </summary>
        /// <param name="param">
        ///     Expected to be an <see cref="IContentLoader" />
        /// </param>
        /// <exception cref="ArgumentException">Throws if param is not an IContentLoader</exception>
        /// <remarks>
        ///     Used as an <see cref="ParameterizedThreadStart" /> in <see cref="executeCall" /> for
        ///     <see cref="RunWithTimeout" />
        /// </remarks>
        private void Call(object param)
        {
            try
            {
                var contentLoader = param as IContentLoader;
                if (contentLoader == null)
                {
                    throw new ArgumentException(@"Expected IContentLoader", nameof(param));
                }
                AppendContent(contentLoader);
            }
            catch (ArgumentException e)
            {
                Logger.Error(e.Message, e);
                _message.Append(e.Message);
            }
            catch (BVSdkException e)
            {
                Logger.Error(e.getMessage(), e);
                _message.Append(e.getMessage());
            }
            
        }

        /// <summary>
        ///     Start a new <see cref="Thread" /> using <paramref name="threadStart" />. If <paramref name="threadStart" /> does
        ///     not complete, the new thread will be aborted.
        /// </summary>
        /// <remarks>
        ///     If the thread is aborted, no guarantees can be made about the state of the data used by
        ///     <paramref name="threadStart" />.
        ///     This method executes synchronously; It will not return until the new thread has been completed or aborted.
        /// </remarks>
        /// <param name="threadStart">Entrypoint for the new thread</param>
        /// <param name="timeout">Maximum time to wait for the new thread to finish</param>
        /// <param name="parameter">This <see cref="object" /> will be pased to <paramref name="threadStart" /></param>
        /// <returns>True iff <paramref name="threadStart" /> completed successfully.</returns>
        private bool RunWithTimeout(ParameterizedThreadStart threadStart, TimeSpan timeout, object parameter)
        {
            var workerThread = new Thread(threadStart);

            workerThread.Start(parameter);
            //TODO: Potentially does not respect the timeout if this thread yields to the new thread before the call to Join(timeout);
            var finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }
    }
}