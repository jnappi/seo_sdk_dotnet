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
using System.Reflection;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;
using log4net;

namespace BVSeoSdkDotNet.Validation
{
    /// <summary>
    /// Validator implementation class for BVParameters & BVConfiguration.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVDefaultValidator : BVValidator
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private StringBuilder errorMessages;

        public BVDefaultValidator()
        {
            errorMessages = new StringBuilder();
        }

        /// <summary>
        /// Method to validate bvConfiguration & bvParameters.
        /// </summary>
        /// <param name="bvConfiguration">The BVConfiguration Object to validate</param>
        /// <param name="bvParams">The BVParameters Object to validate</param>
        /// <returns>Errors as String if Invalid attributes are found</returns>
        public String validate(BVConfiguration bvConfiguration, BVParameters bvParams)
        {
            if (bvConfiguration == null)
            {
                errorMessages.Append(BVMessageUtil.getMessage("ERR0007"));
                _logger.Error(BVMessageUtil.getMessage("ERR0007"));
                return errorMessages.ToString();
            }

            if (bvParams == null)
            {
                errorMessages.Append(BVMessageUtil.getMessage("ERR0011"));
                _logger.Error(BVMessageUtil.getMessage("ERR0011"));
                return errorMessages.ToString();
            }

            Boolean loadSeoFilesLocally = Boolean.Parse(bvConfiguration.getProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY));
            if (loadSeoFilesLocally)
            {
                String localSeoFileRoot = bvConfiguration.getProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT);
                if (String.IsNullOrEmpty(localSeoFileRoot))
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0010"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0010"));
                }
            }
            else
            {
                String cloudKey = bvConfiguration.getProperty(BVClientConfig.CLOUD_KEY);
                if (String.IsNullOrEmpty(cloudKey))
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0020"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0020"));
                }
            }

            String rootFolder = bvConfiguration.getProperty(BVClientConfig.BV_ROOT_FOLDER);
            if (String.IsNullOrEmpty(rootFolder))
            {
                errorMessages.Append(BVMessageUtil.getMessage("ERR0021"));
                _logger.Error(BVMessageUtil.getMessage("ERR0021"));
            }

            if (String.IsNullOrEmpty(bvParams.UserAgent))
            {
                errorMessages.Append(BVMessageUtil.getMessage("ERR0017"));
                _logger.Error(BVMessageUtil.getMessage("ERR0017"));
            }

            Uri uri = null;
            if (bvParams.BaseURI != null)
            {
                try
                {
                    uri = new Uri(bvParams.BaseURI);
                }
                catch (UriFormatException e)
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0023"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0023"),e);
                }
            }

            if (bvParams.PageURI != null)
            {
                try
                {
                    uri = new Uri(bvParams.PageURI);
                }
                catch (UriFormatException e)
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0022"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0022"), e);
                }
            }

            if (String.IsNullOrEmpty(bvParams.PageURI) || !bvParams.PageURI.Contains("bvpage"))
            {
                if (String.IsNullOrEmpty(bvParams.SubjectId))
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0014"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0014"));
                }

                if (bvParams.SubjectType == null)
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0016"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0016"));
                }

                if (bvParams.ContentType == null)
                {
                    errorMessages.Append(BVMessageUtil.getMessage("ERR0015"));
                    _logger.Error(BVMessageUtil.getMessage("ERR0015"));
                }
            }


            if (errorMessages.Length > 0)
            {
                _logger.Error("There is an error : " + errorMessages.ToString());
                return errorMessages.ToString();
            }

            return null;
        }
    }
}
