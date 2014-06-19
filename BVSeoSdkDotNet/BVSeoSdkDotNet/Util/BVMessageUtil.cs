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
using System.Resources;
using BVSeoSdkDotNet.BVException;
using BVSeoSdkDotNet.Properties;

namespace BVSeoSdkDotNet.Util
{
    /// <summary>
    /// Utiltiy class to read and get the message from resource file Resources.resx
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public sealed class BVMessageUtil
    {
        public BVMessageUtil()
        {
        }

        /// <summary>
        /// Gets the message from resource file Resources.resx.
        /// Pass the message code including error code and you should get a message that is in the resource file.
        /// Gives back the same message code if it is not configured in resource bundle.
        /// </summary>
        /// <param name="code">Message Code as String</param>
        /// <returns>Message as String</returns>
        public static String getMessage(String code)
        {
            if (String.IsNullOrEmpty(code))
            {
                throw new BVSdkException("ERR0001");
            }

            String message = null;
            try
            {
                ResourceManager resxMgr = Resources.ResourceManager;
                if (resxMgr != null)
                {
                    string msg = resxMgr.GetString(code);
                    if (msg == null || string.IsNullOrEmpty(msg))
                    {
                        message = code;
                    }
                    else
                    {
                        message = msg;
                    }
                }
                else
                {
                    message = code;
                }
            }
            catch (Exception ex)
            {
                message = code;
            }

            return message;
        }
    }
}
