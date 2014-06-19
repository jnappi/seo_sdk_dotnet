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

namespace BVSeoSdkDotNet.Model
{
    /// <summary>
    /// Model for holding the Bazaarvoice content specific query parameters.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVParameters
    {
        private String _userAgent;
        private String _baseURI;
        private String _pageURI;
        private String _subjectId;
        private BVContentType _contentType;
        private BVSubjectType _subjectType;
        private BVContentSubType _contentSubType;

        /// <summary>
        /// Value of the UserAgent that needs to be included
        /// </summary>
        public String UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// Value of the BaseURI of the initiating URL
        /// </summary>
        public String BaseURI
        {
            get { return _baseURI; }
            set { _baseURI = value; }
        }

        /// <summary>
        /// Value of the PageURI of the initiating URL
        /// </summary>
        public String PageURI
        {
            get { return _pageURI; }
            set { _pageURI = value; }
        }

        /// <summary>
        /// Value of SubjectId of the SEO content
        /// </summary>
        public String SubjectId
        {
            get { return _subjectId; }
            set { _subjectId = value; }
        }

        /// <summary>
        /// BV Content type of the SEO Content to be retrieved
        /// </summary>
        public BVContentType ContentType
        {
            get { return _contentType; }
            set { _contentType = value; }
        }

        /// <summary>
        /// BV Subject type of the SEO Content to be retrieved
        /// </summary>
        public BVSubjectType SubjectType
        {
            get { return _subjectType; }
            set { _subjectType = value; }
        }

        /// <summary>
        /// BV Content Sub type of the SEO Content to be retrieved
        /// </summary>
        public BVContentSubType ContentSubType
        {
            get { return _contentSubType; }
            set { _contentSubType = value; }
        }
    }
}
