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
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVClientConfig
    {
        public static string BV_ROOT_FOLDER = "bv.root.folder";
        public static string CLOUD_KEY = "cloudKey";
        public static string LOAD_SEO_FILES_LOCALLY = "loadSEOFilesLocally";
        public static string LOCAL_SEO_FILE_ROOT = "localSEOFileRoot";
        public static string CONNECT_TIMEOUT = "connectTimeout";
        public static string SOCKET_TIMEOUT = "socketTimeout";
        public static string INCLUDE_DISPLAY_INTEGRATION_CODE = "includeDisplayIntegrationCode";
        public static string BOT_DETECTION = "botDetection";
        public static string CRAWLER_AGENT_PATTERN = "crawlerAgentPattern";
        public static string SEO_SDK_ENABLED = "seo.sdk.enabled";
        public static string STAGING = "staging";
        public static string EXECUTION_TIMEOUT = "seo.sdk.execution.timeout";
        public static string PROXY_HOST = "seo.sdk.execution.proxy.host";
        public static string PROXY_PORT = "seo.sdk.execution.proxy.port";
        public static string SSL_ENABLED = "seo.sdk.ssl.enabled";
        public static string CHARSET = "seo.sdk.charset";

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
            _configs.Add(BOT_DETECTION);
            _configs.Add(CRAWLER_AGENT_PATTERN);
            _configs.Add(SEO_SDK_ENABLED);
            _configs.Add(STAGING);
            _configs.Add(EXECUTION_TIMEOUT);
            _configs.Add(PROXY_HOST);
            _configs.Add(PROXY_PORT);
            _configs.Add(SSL_ENABLED);
            _configs.Add(CHARSET);
            
            return _configs;
        }
    }
}
