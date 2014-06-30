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
    /// Class used or configuring bazaarvoice server configurations. 
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVCoreConfig
    {
        public const string STAGING_S3_HOSTNAME = "stagingS3Hostname";
        public const string PRODUCTION_S3_HOSTNAME = "productionS3Hostname";

        public static List<string> values()
        {
            List<string> _configs = new List<string>();
            _configs.Add(STAGING_S3_HOSTNAME);
            _configs.Add(PRODUCTION_S3_HOSTNAME);

            return _configs;
        }

    }
}
