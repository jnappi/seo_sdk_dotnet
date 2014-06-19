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
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.Url;
using NVelocity.App;
using NVelocity.Context;
using NVelocity;
using BVSeoSdkDotNet.Properties;
using BVSeoSdkDotNet.BVException;

namespace BVSeoSdkDotNet.Footer
{
    /// <summary>
    /// Implementation class for adding bazaarvoice footer in the bazaarvoice seo content.
    /// This class is designed to display footer in the bazaarvoice seo content in HTML formatted tags.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVHTMLFooter : BVFooter
    {
        private static String FOOTER_FILE = "footer.txt"; 
		private BVConfiguration _bvConfiguration;
	    private BVParameters _bvParameters;
        private BVSeoSdkUrl _bvSeoSdkUrl;

        private VelocityEngine _velocityEngine;

        private long executionTime;
        private List<String> messageList;

        public BVHTMLFooter(BVConfiguration bvConfiguration, BVParameters bvParameters)
        {
            if (bvConfiguration == null)
            {
                throw new BVSdkException("ERR0007");
            }

            _bvConfiguration = bvConfiguration;
            _bvParameters = bvParameters;

            _velocityEngine = new VelocityEngine();
            _velocityEngine.Init();

            messageList = new List<String>();
        }

        /// <summary>
        /// Returns the footer based on the configuration that is set.
        /// </summary>
        /// <param name="accessMethod">Parameter to provide the BV SEO Content Access Method</param>
        /// <returns>Footer Content as a String</returns>
        public String displayFooter(String accessMethod) 
        {
            string FooterTemplate = Resources.footer.ToString();
            if (!string.IsNullOrEmpty(FooterTemplate))
            {
                VelocityContext context = new VelocityContext();

                if (_bvParameters != null && _bvParameters.PageURI != null && _bvParameters.PageURI.Contains(BVConstant.BVREVEAL_DEBUG))
                {
                    Hashtable revealMap = null;
                    revealMap = new Hashtable();

                    foreach (string configName in BVCoreConfig.values())
                    {
                        revealMap.Add(configName, _bvConfiguration.getProperty(configName));
                    }

                    foreach (string configName in BVClientConfig.values())
                    {
                        revealMap.Add(configName, _bvConfiguration.getProperty(configName));
                    }
                    context.Put("revealMap", revealMap);
                }

                string assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                String methodType = Boolean.Parse(
                        _bvConfiguration.getProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY)) ? "LOCAL" : "CLOUD";
                context.Put("sdk_enabled", _bvConfiguration.getProperty(BVClientConfig.SEO_SDK_ENABLED));
                context.Put("_bvParameters", _bvParameters);
                context.Put("methodType", methodType);
                context.Put("executionTime", executionTime);
                context.Put("accessMethod", accessMethod);
                context.Put("version", assemblyVersion);
                context.Put("isBd", _bvConfiguration.getProperty(BVClientConfig.BOT_DETECTION));

                String message = null;
                if (messageList != null && messageList.Count > 0)
                {
                    message = "";
                    foreach (String messageStr in messageList)
                    {
                        message += messageStr;
                    }
                }
                context.Put("message", message);

                String url = null;
                Boolean loadFromFile = Boolean.Parse(_bvConfiguration.getProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY));
                if (!loadFromFile && _bvSeoSdkUrl != null)
                {
                    url = _bvSeoSdkUrl.seoContentUri().ToString();
                }
                context.Put("url", url);

                StringWriter writer = new StringWriter();
                _velocityEngine.Evaluate(context,writer, "footer",FooterTemplate);

                return writer.ToString();
            }
            else
            {
                return String.Empty;
            }
	    }

        /// <summary>
        /// Adds a message to the Footer
        /// </summary>
        /// <param name="message">Message as String</param>
        public void addMessage(String message)
        {
            if (!String.IsNullOrEmpty(message))
            {
                this.messageList.Add(message);
            }
        }

        /// <summary>
        /// Set the Execution Time
        /// </summary>
        /// <param name="executionTime">Execution Time as long</param>
        public void setExecutionTime(long executionTime)
        {
            this.executionTime = executionTime;
        }

        /// <summary>
        /// Set the BVSeoSdkUrl
        /// </summary>
        /// <param name="_bvSeoSdkUrl"></param>
        public void setBvSeoSdkUrl(BVSeoSdkUrl _bvSeoSdkUrl)
        {
            this._bvSeoSdkUrl = _bvSeoSdkUrl;
        }
    }
}
