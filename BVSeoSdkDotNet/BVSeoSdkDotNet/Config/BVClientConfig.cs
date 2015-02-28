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

namespace BVSeoSdkDotNet.Config
{
    /// <summary>
    /// Bazaarvoice SEO SDK Client Configuration Values class.
    /// </summary>
    public class BVClientConfig
    {
        public const string BV_ROOT_FOLDER = "bv.root.folder";
        public const string CLOUD_KEY = "cloudKey";
        public const string LOAD_SEO_FILES_LOCALLY = "loadSEOFilesLocally";
        public const string LOCAL_SEO_FILE_ROOT = "localSEOFileRoot";
        public const string CONNECT_TIMEOUT = "connectTimeout";
        public const string SOCKET_TIMEOUT = "socketTimeout";
        public const string INCLUDE_DISPLAY_INTEGRATION_CODE = "includeDisplayIntegrationCode";
        public const string CRAWLER_AGENT_PATTERN = "crawlerAgentPattern";
        public const string SEO_SDK_ENABLED = "seo.sdk.enabled";
        public const string STAGING = "staging";
        public const string TESTING = "testing";
        public const string EXECUTION_TIMEOUT = "seo.sdk.execution.timeout";
        public const string EXECUTION_TIMEOUT_BOT = "seo.sdk.execution.timeout.bot";
        public const string PROXY_HOST = "seo.sdk.execution.proxy.host";
        public const string PROXY_PORT = "seo.sdk.execution.proxy.port";
        public const string SSL_ENABLED = "seo.sdk.ssl.enabled";
        public const string CHARSET = "seo.sdk.charset";

        public static List<string> values()
        {
            List<string> _configs = new List<string>();
            _configs.Add(BV_ROOT_FOLDER);
            _configs.Add(CLOUD_KEY);
            _configs.Add(LOAD_SEO_FILES_LOCALLY);
            _configs.Add(LOCAL_SEO_FILE_ROOT);
            _configs.Add(CONNECT_TIMEOUT);
            _configs.Add(SOCKET_TIMEOUT);
            _configs.Add(INCLUDE_DISPLAY_INTEGRATION_CODE);
            _configs.Add(CRAWLER_AGENT_PATTERN);
            _configs.Add(SEO_SDK_ENABLED);
            _configs.Add(STAGING);
            _configs.Add(TESTING);
            _configs.Add(EXECUTION_TIMEOUT);
            _configs.Add(EXECUTION_TIMEOUT_BOT);
            _configs.Add(PROXY_HOST);
            _configs.Add(PROXY_PORT);
            _configs.Add(SSL_ENABLED);
            _configs.Add(CHARSET);
            
            return _configs;
        }
    }
}
