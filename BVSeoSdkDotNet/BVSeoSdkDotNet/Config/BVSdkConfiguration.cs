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
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.BVException;
using log4net;

namespace BVSeoSdkDotNet.Config
{
    /// <summary>
    /// Default implementation of configuration settings. This loads the Bazaarvoice
    /// specific configuration and also user specific setting.
    /// </summary>
    public class BVSdkConfiguration : BVConfiguration
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Dictionary<String, String> _instanceConfiguration;

        /// <summary>
        /// Default constructor. If configuration should be overwritten or if you
        /// need to load properties/configuration every time please use the add method.
        /// </summary>
        public BVSdkConfiguration()
        {
            _instanceConfiguration = new Dictionary<String, String>();

            _instanceConfiguration.Add(BVCoreConfig.PRODUCTION_S3_HOSTNAME, BVConstant.PRODUCTION_S3_HOSTNAME);
            _instanceConfiguration.Add(BVCoreConfig.STAGING_S3_HOSTNAME, BVConstant.STAGING_S3_HOSTNAME);

            addProperty(BVClientConfig.EXECUTION_TIMEOUT, BVConstant.EXECUTION_TIMEOUT);
            addProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT, BVConstant.EXECUTION_TIMEOUT_BOT);
            addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, BVConstant.CRAWLER_AGENT_PATTERN);
            addProperty(BVClientConfig.CONNECT_TIMEOUT, BVConstant.CONNECT_TIMEOUT);
            addProperty(BVClientConfig.SOCKET_TIMEOUT, BVConstant.SOCKET_TIMEOUT);
            addProperty(BVClientConfig.STAGING, BVConstant.STAGING);
            addProperty(BVClientConfig.SEO_SDK_ENABLED, BVConstant.SEO_SDK_ENABLED);
            addProperty(BVClientConfig.PROXY_HOST, BVConstant.PROXY_HOST);
            addProperty(BVClientConfig.PROXY_PORT, BVConstant.PROXY_PORT);
            addProperty(BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE, BVConstant.INCLUDE_DISPLAY_INTEGRATION_CODE);
            addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, BVConstant.LOAD_SEO_FILES_LOCALLY);
            addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, BVConstant.LOCAL_SEO_FILE_ROOT);
            addProperty(BVClientConfig.CLOUD_KEY, BVConstant.CLOUD_KEY);
            addProperty(BVClientConfig.BV_ROOT_FOLDER, BVConstant.BV_ROOT_FOLDER);
            addProperty(BVClientConfig.SSL_ENABLED, BVConstant.SSL_ENABLED);
            addProperty(BVClientConfig.CHARSET, BVConstant.CHARSET);
            _logger.Debug("Completed default properties in BVSdkConfiguration.");
        }

        /// <summary>
        /// Add the Value for the Property to the Configurations
        /// </summary>
        /// <param name="bvConfig">Config Property Name</param>
        /// <param name="propertyValue">Config Property Value</param>
        /// <returns></returns>
        public BVConfiguration addProperty(string bvConfig, String propertyValue)
        {
            if (String.IsNullOrEmpty(propertyValue))
            {
                throw new BVSdkException("ERR0006");
            }
            if (!this._instanceConfiguration.ContainsKey(bvConfig))
            {
                this._instanceConfiguration.Add(bvConfig, propertyValue);
            }
            else
            {
                this._instanceConfiguration[bvConfig] = propertyValue;
            }
            return this;
        }

        /// <summary>
        /// Retrieve the Value for the Property to the Configurations
        /// </summary>
        /// <param name="propertyName">Config Property Name</param>
        /// <returns>Config Property Value as String</returns>
        public String getProperty(String propertyName)
        {
            if (this._instanceConfiguration.ContainsKey(propertyName))
            {
                return this._instanceConfiguration[propertyName];
            }
            return null;
        }
    }
}
