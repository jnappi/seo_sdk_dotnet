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
using System.IO;
using BVSeoSdkDotNet.Util;

namespace BVSeoSdkDotNet.BVException
{
    /// <summary>
    /// Bazaarvoice sdk exception class.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVSdkException : Exception
    {
        private static long serialVersionUID = 1;

	    private String errorCode;
	
	    public BVSdkException(String errorCode) : base()
        {
		    this.errorCode = errorCode;
	    }
	
	    public BVSdkException(String errorCode, IOException e) : base(errorCode,e)
        {
            this.errorCode = errorCode;
	    }

        /// <summary>
        /// Get the Message for the ErrorCode
        /// </summary>
        /// <returns>Message as String</returns>
        public String getMessage()
        {
            if (!String.IsNullOrEmpty(errorCode))
            {
                return BVMessageUtil.getMessage(errorCode);
            }

            return base.Message;
        }
    }
}
