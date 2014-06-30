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
    /// Class to support ContentType : Allows to speciy the type of contents to get from the cloud.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVContentType
    {
        /// <summary>
        /// REVIEWS
        /// </summary>
        public const string REVIEWS = "re";

        /// <summary>
        /// REVIEWSPAGE
        /// </summary>
        public const string REVIEWSPAGE = "rp";

        /// <summary>
        /// QUESTIONS
        /// </summary>
        public const string QUESTIONS = "qa";

        /// <summary>
        /// QUESTIONSPAGE
        /// </summary>
        public const string QUESTIONSPAGE = "qp";

        /// <summary>
        /// STORIES
        /// </summary>
        public const string STORIES = "sy";

        /// <summary>
        /// STORIESPAGE
        /// </summary>
        public const string STORIESPAGE = "sp";

        /// <summary>
        /// UNIVERSAL
        /// </summary>
        public const string UNIVERSAL = "un";


        private String _bvContentType;

        public BVContentType(String bvContentType)
        {
            this._bvContentType = bvContentType;
        }

        public String uriValue() {
            if (this._bvContentType.Equals(REVIEWS, StringComparison.OrdinalIgnoreCase))
            {
                return "REVIEWS".ToLower();
            }

            if (this._bvContentType.Equals(REVIEWSPAGE, StringComparison.OrdinalIgnoreCase))
            {
                return "REVIEWSPAGE".ToLower();
            }

            if (this._bvContentType.Equals(QUESTIONS, StringComparison.OrdinalIgnoreCase))
            {
                return "QUESTIONS".ToLower();
            }

            if (this._bvContentType.Equals(QUESTIONSPAGE, StringComparison.OrdinalIgnoreCase))
            {
                return "QUESTIONSPAGE".ToLower();
            }

            if (this._bvContentType.Equals(STORIES, StringComparison.OrdinalIgnoreCase))
            {
                return "STORIES".ToLower();
            }

            if (this._bvContentType.Equals(STORIESPAGE, StringComparison.OrdinalIgnoreCase))
            {
                return "STORIESPAGE".ToLower();
            }

            if (this._bvContentType.Equals(UNIVERSAL, StringComparison.OrdinalIgnoreCase))
            {
                return "UNIVERSAL".ToLower();
            }

            return null;
        }

        public String getIntegrationScriptProperty() {
            return this._bvContentType + "Script";
        }
        
        public static String ctFromKeyWord(String ctxKeyWord) {
    	    if (ctxKeyWord.Equals("re", StringComparison.OrdinalIgnoreCase)) 
            {
    		    return REVIEWS;
    	    }

            if (ctxKeyWord.Equals("rp", StringComparison.OrdinalIgnoreCase))
            {
    		    return REVIEWSPAGE;
    	    }

            if (ctxKeyWord.Equals("qa", StringComparison.OrdinalIgnoreCase))
            {
    		    return QUESTIONS;
    	    }

            if (ctxKeyWord.Equals("qp", StringComparison.OrdinalIgnoreCase))
            {
    		    return QUESTIONSPAGE;
    	    }

            if (ctxKeyWord.Equals("sy", StringComparison.OrdinalIgnoreCase))
            {
    		    return STORIES;
    	    }

            if (ctxKeyWord.Equals("un", StringComparison.OrdinalIgnoreCase))
            {
    		    return UNIVERSAL;
    	    }
        	
    	    return null;
        }

    }
}
