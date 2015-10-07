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
    /// Class to support SubjectType : Added for Product, Category, Entry and Detail types
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVSubjectType
    {
        /// <summary>
        /// PRODUCT
        /// </summary>
        public const string PRODUCT = "p";

        /// <summary>
        /// CATEGORY
        /// </summary>
        public const string CATEGORY = "c";

        /// <summary>
        /// ENTRY
        /// </summary>
        public const string ENTRY = "e";

        /// <summary>
        /// DETAIL
        /// </summary>
        public const string DETAIL = "d";

        /// <summary>
        /// SELLER
        /// </summary>
        public const string SELLER = "s";

        private String cs2013Text;
    
        public BVSubjectType(String cs2013Text) {
    	    this.cs2013Text = cs2013Text;
        }
        
        public String uriValue() {
            if (this.cs2013Text.Equals("p", StringComparison.OrdinalIgnoreCase))
            {
                return "product";
            }

            if (this.cs2013Text.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
                return "category";
            }

            if (this.cs2013Text.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
                return "entry";
            }

            if (this.cs2013Text.Equals("d", StringComparison.OrdinalIgnoreCase))
            {
                return "detail";
            }

            if (this.cs2013Text.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                return "seller";
            }

            return null;
        }
        
        public String getCS2013Text() {
    	    return this.cs2013Text;
        }
        
        public static String subjectType(String subjectType) {
            if (subjectType.Equals("p", StringComparison.OrdinalIgnoreCase))
            {
    		    return PRODUCT;
    	    }

            if (subjectType.Equals("c", StringComparison.OrdinalIgnoreCase))
            {
    		    return CATEGORY;
    	    }

            if (subjectType.Equals("e", StringComparison.OrdinalIgnoreCase))
            {
    		    return ENTRY;
    	    }

            if (subjectType.Equals("d", StringComparison.OrdinalIgnoreCase))
            {
    		    return DETAIL;
            }

            if (subjectType.Equals("s", StringComparison.OrdinalIgnoreCase))
            {
                return SELLER;
            }
        	
    	    return null;
        }
    }
}
