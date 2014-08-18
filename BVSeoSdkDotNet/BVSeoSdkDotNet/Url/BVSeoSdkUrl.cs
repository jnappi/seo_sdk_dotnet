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

namespace BVSeoSdkDotNet.Url
{
    /// <summary>
    /// Interface to frame bazaarvoice url.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public interface BVSeoSdkUrl
    {
        /// <summary>
        /// Forms either an http URL or file URL to access the content.
        /// </summary>
        /// <returns>Returns the URL in URI format</returns>
        Uri seoContentUri();

        /// <summary>
        /// Retrieves the query string portion from the URL.
        /// </summary>
        /// <returns>Returns the Query String</returns>
        String queryString();

        /// <summary>
        /// Corrects the base uri from bvParameters and returns the baseUri which is corrected.
        /// </summary>
        /// <returns>Corrected Base URI as String</returns>
        String correctedBaseUri();
    }
}
