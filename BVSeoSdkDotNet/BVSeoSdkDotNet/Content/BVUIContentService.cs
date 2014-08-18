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
using BVSeoSdkDotNet.Url;
using BVSeoSdkDotNet.Model;

namespace BVSeoSdkDotNet.Content
{
    /// <summary>
    /// BVUIContentService is an Interface for the SEO Content Service
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public interface BVUIContentService
    {
        /// <summary>
        /// Implementation to check if sdk is enabled/disabled.
        /// The settings are based on the configurations from BVConfiguration and BVParameters.
        /// </summary>
        /// <returns>A Boolean value, true if sdk is enabled and false if it is not enabled</returns>
        Boolean isSdkEnabled();

        /// <summary>
        /// Executes the server side call or the file level call within a specified execution timeout.
        /// when reload is set true then it gives from the cache that was already executed in the previous call.
        /// </summary>
        /// <param name="reload">A Boolean value to determine whether to reload from cache</param>
        /// <returns>A StringBuilder object representing the Content</returns>
        StringBuilder executeCall(Boolean reload);

        /// <summary>
        /// Gets the messages if there are any after executeCall is invoked or if it is still in the cache.
        /// </summary>
        /// <returns>A StringBuilder object containing the messages if any</returns>
        StringBuilder getMessage();

        /// <summary>
        /// Gets a boolean value whether to Show UserAgent SEO Content
        /// </summary>
        /// <returns>A boolean value</returns>
        Boolean showUserAgentSEOContent();

        /// <summary>
        /// Sets the BV Parameters for retreiving the SEO content
        /// </summary>
        /// <param name="bvParameters">Parameters that decide the SEO Content</param>
        void setBVParameters(BVParameters bvParameters);

        /// <summary>
        /// Set the URL for the SEO SDK.
        /// </summary>
        /// <param name="bvSeoSdkUrl"></param>
        void setBVSeoSdkUrl(BVSeoSdkUrl bvSeoSdkUrl);
    }
}
