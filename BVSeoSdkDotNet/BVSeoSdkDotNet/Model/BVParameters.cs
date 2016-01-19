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

using BVSeoSdkDotNet.Content;

namespace BVSeoSdkDotNet.Model
{
    /// <summary>
    ///     Model for holding the Bazaarvoice content specific query parameters.
    ///     @author Mohan Krupanandan
    /// </summary>
    public class BVParameters
    {
        public BVParameters()
        {
            ContentType = new BVContentType(BVContentType.REVIEWS);
            SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
        }

        /// <summary>
        ///     Default value of the UserAgent to use in requests for content.
        /// </summary>
        /// <remarks>
        ///     Should be set if the call to <see cref="BVManagedUIContent.getContent" />,
        ///     <see cref="BVManagedUIContent.getReviews" />, or <see cref="BVManagedUIContent.getAggregateRating" /> will not be
        ///     made on the request thread.
        /// </remarks>
        public string UserAgent { get; set; }

        /// <summary>
        ///     Value of the BaseURI of the initiating URL
        /// </summary>
        public string BaseURI { get; set; }

        /// <summary>
        ///     Value of the PageURI of the initiating URL
        /// </summary>
        public string PageURI { get; set; }

        /// <summary>
        ///     Value of SubjectId of the SEO content
        /// </summary>
        public string SubjectId { get; set; }

        /// <summary>
        ///     BV Content type of the SEO Content to be retrieved
        /// </summary>
        public BVContentType ContentType { get; set; }

        /// <summary>
        ///     BV Subject type of the SEO Content to be retrieved
        /// </summary>
        public BVSubjectType SubjectType { get; set; }

        /// <summary>
        ///     BV Content Sub type of the SEO Content to be retrieved
        /// </summary>
        public BVContentSubType ContentSubType { get; set; }

        public string PageNumber { get; set; }
    }
}