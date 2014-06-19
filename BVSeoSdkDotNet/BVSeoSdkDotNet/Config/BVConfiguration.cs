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
    /// Bazaarvoice SEO SDK Configuration Interface to get or set configurations.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public interface BVConfiguration
    {
        /// <summary>
        /// Add the Value for the Property to the Configurations
        /// </summary>
        /// <param name="bvConfig">Config Property Name</param>
        /// <param name="propertyValue">Config Property Value</param>
        /// <returns></returns>
        BVConfiguration addProperty(String bvConfig, String propertyValue);

        /// <summary>
        /// Retrieve the Value for the Property to the Configurations
        /// </summary>
        /// <param name="propertyName">Config Property Name</param>
        /// <returns>Config Property Value as String</returns>
        String getProperty(String propertyName);
    }
}
