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
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Footer;
using BVSeoSdkDotNet.Url;
using BVSeoSdkDotNet.Validation;
using BVSeoSdkDotNet.Util;
using log4net;

namespace BVSeoSdkDotNet.Content
{
    /// <summary>
    /// Implementation class for BVUIContent.
    /// This class is the default implementation class to get Bazaarvoice content.
    /// Based on the configurations that are set, the actual contents will be retrieved.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public class BVManagedUIContent : BVUIContent
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private BVConfiguration _bvConfiguration;
        private BVValidator bvParamValidator;

        private BVSeoSdkUrl bvSeoSdkUrl;
        private BVFooter bvFooter;
        private StringBuilder message;
        private BVParameters bvParameters;
        private Boolean reloadContent;
        private BVUIContentService bvUiContentService;
        private String validationError;

        /// <summary>
        /// Default constructor.
        /// Loads all default configuration within.
        /// </summary>
        public BVManagedUIContent() : this(null)
        {   
        }

        /// <summary>
        /// Constructor with BVConfiguration argument.
        /// </summary>
        /// <param name="bvConfiguration">The configuration/settings that has to be provided</param>
        public BVManagedUIContent(BVConfiguration bvConfiguration)
        {
            this._bvConfiguration = bvConfiguration;

            if (bvConfiguration == null)
            {
                this._bvConfiguration = new BVSdkConfiguration();
            }
        }

        /// <summary>
        /// Gets the complete Bazaarvoice SEO content
        /// </summary>
        /// <param name="bvParameters">Query Parameters of type BVParameters</param>
        /// <returns>Returns the complete Bazaarvoice SEO Content as String</returns>
        public String getContent(BVParameters bvParameters)
        {
            long startTime = TimeinMilliSeconds();
            postProcess(bvParameters);

            StringBuilder uiContent = null;

            if (String.IsNullOrEmpty(validationError))
            {
                if (bvUiContentService.isSdkEnabled())
                {
                    uiContent = bvUiContentService.executeCall(reloadContent);
                }
                else
                {
                    _logger.Info(BVMessageUtil.getMessage("MSG0003"));
                    uiContent = new StringBuilder();
                }
                bvFooter.addMessage(bvUiContentService.getMessage().ToString());
            }
            else
            {
                uiContent = new StringBuilder();
                bvFooter.addMessage(validationError);
            }

            bvFooter.setExecutionTime(TimeinMilliSeconds() - startTime);
            uiContent.Append(bvFooter.displayFooter("getContent"));

            return uiContent.ToString();
        }

        /// <summary>
        /// Gets only the AggregateRating section of the Bazaarvoice SEO Content
        /// </summary>
        /// <param name="bvQueryParams">Query Parameters of type BVParameters</param>
        /// <returns>Returns the AggregateRating section of the Bazaarvoice SEO Content as String</returns>
        public String getAggregateRating(BVParameters bvQueryParams)
        {
            long startTime = TimeinMilliSeconds();
            postProcess(bvQueryParams);

            StringBuilder uiContent = null;
            if (String.IsNullOrEmpty(validationError))
            {
                if (bvUiContentService.isSdkEnabled())
                {
                    uiContent = bvUiContentService.executeCall(reloadContent);
                }
                else
                {
                    _logger.Info(BVMessageUtil.getMessage("MSG0003"));
                    uiContent = new StringBuilder();
                }

                int startIndex = uiContent.ToString().IndexOf("<!--begin-reviews-->");
                if (startIndex == -1)
                {
                    if (bvUiContentService.showUserAgentSEOContent() &&
                            bvUiContentService.getMessage().Length == 0 && bvUiContentService.isSdkEnabled())
                    {
                        String messageString = BVMessageUtil.getMessage("ERR0003");
                        _logger.Error(BVMessageUtil.getMessage("ERR0003"));
                        message.Append(messageString);
                    }
                }
                else
                {
                    String endReviews = "<!--end-reviews-->";
                    int endIndex = uiContent.ToString().IndexOf(endReviews) + endReviews.Length;
                    int Length = endIndex - startIndex;
                    uiContent.Remove(startIndex, Length);

                    startIndex = uiContent.ToString().IndexOf("<!--begin-pagination-->");
                    if (startIndex != -1)
                    {
                        String endPagination = "<!--end-pagination-->";
                        endIndex = uiContent.ToString().IndexOf(endPagination) + endPagination.Length;
                        Length = endIndex - startIndex;
                        uiContent.Remove(startIndex,Length);
                    }
                }
                bvFooter.addMessage(bvUiContentService.getMessage().ToString());
            }
            else
            {
                uiContent = new StringBuilder();
                bvFooter.addMessage(validationError);
            }


            bvFooter.addMessage(message.ToString());
            bvFooter.setExecutionTime(TimeinMilliSeconds() - startTime);
            uiContent.Append(bvFooter.displayFooter("getAggretateRating"));

            return uiContent.ToString();
        }

        /// <summary>
        /// Gets only the Reviews section of the Bazaarvoice SEO Content
        /// </summary>
        /// <param name="bvQueryParams">Query Parameters of type BVParameters</param>
        /// <returns>Returns the Reviews section of the Bazaarvoice SEO Content as String</returns>
        public String getReviews(BVParameters bvQueryParams)
        {
            int startIndex;
            long startTime = TimeinMilliSeconds();
            postProcess(bvQueryParams);

            StringBuilder uiContent = null;
            if (String.IsNullOrEmpty(validationError))
            {

                if (bvUiContentService.isSdkEnabled())
                {
                    uiContent = bvUiContentService.executeCall(reloadContent);
                }
                else
                {
                    _logger.Info(BVMessageUtil.getMessage("MSG0003"));
                    uiContent = new StringBuilder();
                }

                startIndex = uiContent.ToString().IndexOf("<!--begin-aggregate-rating-->");

                if (startIndex == -1)
                {
                    if (bvUiContentService.showUserAgentSEOContent() &&
                            bvUiContentService.getMessage().Length == 0 && bvUiContentService.isSdkEnabled())
                    {
                        String messageString = BVMessageUtil.getMessage("ERR0013");
                        _logger.Error(BVMessageUtil.getMessage("ERR0013"));
                        message.Append(messageString);
                    }
                }
                else
                {
                    String endReviews = "<!--end-aggregate-rating-->";
                    int endIndex = uiContent.ToString().IndexOf(endReviews) + endReviews.Length;
                    int Length = endIndex - startIndex;
                    uiContent.Remove(startIndex, Length);
                }
                bvFooter.addMessage(bvUiContentService.getMessage().ToString());
                bvFooter.addMessage(message.ToString());
            }
            else
            {
                uiContent = new StringBuilder();
                bvFooter.addMessage(validationError);
            }


            /*
             * Remove schema.org text from reviews if one exists
             * itemscope itemtype="http://schema.org/Product"
             */
            String schemaOrg = "itemscope itemtype=\"http://schema.org/Product\"";
            startIndex = uiContent.ToString().IndexOf(schemaOrg);
            if (startIndex != -1)
            {
                uiContent.Remove(startIndex, schemaOrg.Length);
            }

            bvFooter.setExecutionTime(TimeinMilliSeconds() - startTime);
            uiContent.Append(bvFooter.displayFooter("getReviews"));

            return uiContent.ToString();
        }

        private void postProcess(BVParameters bvParameters)
        {
            bvFooter = new BVHTMLFooter(_bvConfiguration, bvParameters);
            message = new StringBuilder();

            /*
             * Validator to check if all the bvParameters are valid.
             */
            bvParamValidator = new BVDefaultValidator();
            validationError = bvParamValidator.validate(_bvConfiguration, bvParameters);

            if (!String.IsNullOrEmpty(validationError))
            {
                return;
            }

            reloadContent = bvParameters.Equals(this.bvParameters);

            if (!reloadContent)
            {

                this.bvParameters = bvParameters;

                bvSeoSdkUrl = new BVSeoSdkURLBuilder(_bvConfiguration, bvParameters);

                bvUiContentService = new BVUIContentServiceProvider(_bvConfiguration);
                bvUiContentService.setBVParameters(this.bvParameters);
                bvUiContentService.setBVSeoSdkUrl(bvSeoSdkUrl);
                bvFooter.setBvSeoSdkUrl(bvSeoSdkUrl);
            }

        }

        private long TimeinMilliSeconds()
        {
            long ms1 = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
            return ms1;
        }
    }
}
