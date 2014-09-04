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

namespace BVSeoSdkDotNet.Util
{
    /// <summary>
    /// Common constants class used through out SEO SDK implementations.
    /// </summary>
    public sealed class BVConstant
    {
        public const String BVREVEAL = "bvreveal";
	    public const String BVREVEAL_DEBUG = "bvreveal=debug";
	    public const String INCLUDE_PAGE_URI = "{INSERT_PAGE_URI}";
    	
        /// <summary>
        /// Some of the Default Property Values
        /// </summary>
	    public const String STAGING_S3_HOSTNAME = "seo-stg.bazaarvoice.com"; 
	    public const String PRODUCTION_S3_HOSTNAME = "seo.bazaarvoice.com";
	    public const String EXECUTION_TIMEOUT = "500";
        public const String EXECUTION_TIMEOUT_BOT = "2000";
	    public const String CRAWLER_AGENT_PATTERN = "msnbot|google|teoma|bingbot|yandexbot|yahoo";
	    public const String CONNECT_TIMEOUT = "1000";
	    public const String SOCKET_TIMEOUT = "1000";
	    public const String STAGING = "false";
	    public const String SEO_SDK_ENABLED = "true";
        public const String PROXY_HOST = "none";
        public const String PROXY_PORT = "0";
        public const String INCLUDE_DISPLAY_INTEGRATION_CODE = "false";
        public const String LOAD_SEO_FILES_LOCALLY = "false";
        public const String LOCAL_SEO_FILE_ROOT = "/";
        public const String CLOUD_KEY = "cloudKey";
        public const String BV_ROOT_FOLDER = "rootFolder";
        public const String SSL_ENABLED = "false";
        public const String CHARSET = "UTF-8";
        public const String BV_STRING_PATTERN = "BV";
    }
}
