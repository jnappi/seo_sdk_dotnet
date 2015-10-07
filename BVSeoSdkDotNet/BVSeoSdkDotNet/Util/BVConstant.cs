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
        public const String BVSTATE_REGEX = "bvstate=((([^?&/%:])*:([^?&/%])*)(/)?)+";
        // Add & or %26 to be extracted from the end of bvstate parameter
        public const String BVSTATE_REGEX_WITH_TRAILING_SEPERATOR = BVSTATE_REGEX + "(&|%26){0,1}";
        public const String BVSTATE_REVEAL_DEBUG_REGEX = "bvstate=((([^?&/%:])*:([^?&/%])*)(/)?)+(reveal:debug)";
        public const Char BVSTATE_KEYVALUE_SEPARATOR_CHAR = ':';
        public const Char BVSTATE_TOKEN_SEPARATOR_CHAR = '/';
        public const String ESCAPED_FRAGMENT_KEY = "_escaped_fragment_=";
        public const String ESCAPED_FRAGMENT_MULTIVALUE_SEPERATOR = "%26";
        public const String FRAGMENT_MARKER = "#!";
        public const String INCLUDE_PAGE_URI = "{INSERT_PAGE_URI}";
    	
        /// <summary>
        /// Some of the Default Property Values
        /// </summary>
	    public const String STAGING_S3_HOSTNAME = "seo-stg.bazaarvoice.com"; 
	    public const String PRODUCTION_S3_HOSTNAME = "seo.bazaarvoice.com";
        public const String TESTING_STAGING_S3_HOSTNAME = "seo-qa-stg.bazaarvoice.com";
        public const String TESTING_PRODUCTION_S3_HOSTNAME = "seo-qa.bazaarvoice.com";
        public const String SELLER_RATINGS_S3_HOSTNAME = "srd.bazaarvoice.com";
        public const String SELLER_RATINGS_DEFAULT_SUBJECT_ID = "seller";
        public const String ENVIRONMENT_PROD = "prod";
        public const String ENVIRONMENT_STAGING = "stg";
        public const String ENVIRONMENT_TESTING = "qa";
        public const String ENVIRONMENT_TESTING_STAGING = "qa-stg";
	    public const String EXECUTION_TIMEOUT = "500";
        public const String EXECUTION_TIMEOUT_BOT = "2000";
	    public const String CRAWLER_AGENT_PATTERN = "msnbot|google|teoma|bingbot|yandexbot|yahoo";
	    public const String CONNECT_TIMEOUT = "2000";
	    public const String SOCKET_TIMEOUT = "2000";
	    public const String STAGING = "false";
        public const String TESTING = "false";
	    public const String SEO_SDK_ENABLED = "true";
        public const String PROXY_HOST = "none";
        public const String PROXY_PORT = "0";
        public const String INCLUDE_DISPLAY_INTEGRATION_CODE = "false";
        public const String LOAD_SEO_FILES_LOCALLY = "false";
        public const String LOCAL_SEO_FILE_ROOT = "/";
        public const String CLOUD_KEY = "cloudKey";
        public const String BV_ROOT_FOLDER = "rootFolder";
        public const String BV_SPOTLIGHTS_ROOT_FOLDER = "rootFolder";
        public const String SSL_ENABLED = "false";
        public const String CHARSET = "UTF-8";
        public const String BV_STRING_PATTERN = "BV";
        public const String BV_SPOTLIGHTS_SUB_FOLDER ="spotlights/category";
    }
}
