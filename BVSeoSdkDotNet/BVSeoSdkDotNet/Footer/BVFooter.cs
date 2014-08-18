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

namespace BVSeoSdkDotNet.Footer
{
    /// <summary>
    /// Interface for adding bazaarvoice footer in the bazaarvoice seo content.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public interface BVFooter
    {
        /// <summary>
        /// Returns the footer based on the configuration that is set.
        /// </summary>
        /// <param name="accessMethod">Parameter to provide the BV SEO Content Access Method</param>
        /// <returns>Footer Content as a String</returns>
        String displayFooter(String accessMethod);

        /// <summary>
        /// Adds a message to the Footer
        /// </summary>
        /// <param name="message">Message as String</param>
        void addMessage(String message);

        /// <summary>
        /// Set the Execution Time
        /// </summary>
        /// <param name="executionTime">Execution Time as long</param>
        void setExecutionTime(long executionTime);

        /// <summary>
        /// Set the BVSeoSdkUrl
        /// </summary>
        /// <param name="_bvSeoSdkUrl"></param>
        void setBvSeoSdkUrl(BVSeoSdkUrl _bvSeoSdkUrl);
    }
}
