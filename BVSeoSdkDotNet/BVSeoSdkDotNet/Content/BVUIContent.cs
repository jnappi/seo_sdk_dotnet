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
using BVSeoSdkDotNet.Model;

namespace BVSeoSdkDotNet.Content
{
    /// <summary>
    /// Interface for Bazaarvoice SEO Content Management
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public interface BVUIContent
    {
        /// <summary>
        /// Gets the complete Bazaarvoice SEO content.
        /// </summary>
        /// <param name="bvQueryParams">Query Parameters of type BVParameters</param>
        /// <returns>Returns the complete Bazaarvoice SEO Content as String</returns>
        String getContent(BVParameters bvQueryParams);

        /// <summary>
        /// Gets only the AggregateRating section of the Bazaarvoice SEO Content
        /// </summary>
        /// <param name="bvQueryParams">Query Parameters of type BVParameters</param>
        /// <returns>Returns the AggregateRating section of the Bazaarvoice SEO Content as String</returns>
        String getAggregateRating(BVParameters bvQueryParams);

        /// <summary>
        /// Gets only the Reviews section of the Bazaarvoice SEO Content
        /// </summary>
        /// <param name="bvQueryParams">Query Parameters of type BVParameters</param>
        /// <returns>Returns the Reviews section of the Bazaarvoice SEO Content as String</returns>
        String getReviews(BVParameters bvQueryParams);

       
        /// <summary>
        /// provides the Url for reference.
        /// </summary>
        /// <returns></returns>
        String getUrl();

    }
}
