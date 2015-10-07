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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Reflection;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.BVException;
using log4net;

namespace BVSeoSdkDotNet.Url
{
    /// <summary>
    /// Class to support building the proper url to access the bazaarvoice content
    /// </summary>
    public class BVSeoSdkURLBuilder : BVSeoSdkUrl
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const String BV_PAGE = "bvpage";
        private const String BV_STATE = "bvstate";
        private const String ESCAPED_FRAGMENT = "_escaped_fragment_=";
	    private const String NUM_ONE_STR = "1";
	    private const String HTML_EXT = ".htm";
        private const String PATH_SEPARATOR = "/";

        private BVConfiguration bvConfiguration;
        private BVParameters bvParameters;
        private String _fragmentString;
        private String _queryString;

        public BVSeoSdkURLBuilder(BVConfiguration bvConfiguration, BVParameters bvParameters)
        {
            this.bvConfiguration = bvConfiguration;
            this.bvParameters = bvParameters;
            this._queryString = queryString();
        }

        /// <summary>
        /// Corrects the base uri from bvParameters and returns the baseUri which is corrected.
        /// </summary>
        /// <returns>Corrected Base URI as String</returns>
        public String correctedBaseUri()
        {
            return BVUtility.removeBVParameters
            (
                (bvParameters.BaseURI == null) ? "" : bvParameters.BaseURI
            );
        }

        /// <summary>
        /// Retrieves the query string portion from the URL.
        /// </summary>
        /// <returns>Returns the Query String</returns>
        public String queryString()
        {
            if (this._queryString == null)
            {
                this._queryString = BVUtility.getQueryString(bvParameters.PageURI);
            }
            return this._queryString;
        }

        /// <summary>
        /// Retrieves the fragment string portion from the URL.
        /// </summary>
        /// <returns>Returns the Fragment String</returns>
        public String fragmentString()
        {
            if (this._fragmentString == null)
            {
                this._fragmentString = BVUtility.getFragmentString(bvParameters.PageURI);
            }
            return this._fragmentString;
        }

        /// <summary>
        /// Forms either an http URL or file URL to access the content.
        /// </summary>
        /// <returns>Returns the URL in URI format</returns>
        public Uri seoContentUri()
        {
            Uri uri = null;
            // Extract from bvstate
            if
            (
                bvParameters.PageURI != null &&
                bvParameters.PageURI.Contains(BV_STATE)
            )
            {
                Regex regex = new Regex(BVConstant.BVSTATE_REGEX);
                // Url Fragment has highest priority for bvstate
                String fragment = fragmentString();

                if (fragment.Contains(BV_STATE))
                {
                    Match match = regex.Match(fragment);
                    if (match.Success)
                    {
                        uri = bvstateUri(match.Value);
                    }
                }

                if (uri == null)
                {
                    // Try bvstate in ESCAPED_FRAGMENT
                    String query = queryString();
                    if (query.Contains(ESCAPED_FRAGMENT))
                    {
                        String escapedFragmentValue = Regex.Split(
                            query,
                            ESCAPED_FRAGMENT
                        )[1];
                        if
                        (
                            escapedFragmentValue != null &&
                            escapedFragmentValue.Length > 0
                        )
                        {
                            Match match = regex.Match(escapedFragmentValue);
                            if (match.Success)
                            {
                                uri = bvstateUri(match.Value);
                            }
                        }
                    }

                    // Try bvstate in query parameter
                    if(uri == null)
                    {
                        Match match = regex.Match(queryString());
                        if (match.Success)
                        {
                            uri = bvstateUri(match.Value);
                        }
                    }
                }
            }
            if (uri != null)
            {
                return uri;
            }

            // Fallback to extract from bvpage
            // We can think about removing this when we no longer support 
            if (_queryString != null && _queryString.Contains(BV_PAGE))
            {
                return c2013Uri();
            }
            // Fallback to old legacy seo parameters
            // This can be removed when we no longer support old seo parameters
            return prrUri();
        }

        private Uri prrUri()
        {
            setPageNumber();
            String path = getPath(bvParameters.ContentType, bvParameters.SubjectType, bvParameters.PageNumber, bvParameters.SubjectId, bvParameters.ContentSubType);
            if (isContentFromFile())
            {
                return fileUri(path);
            }
            return httpUri(path);
        }

        private Uri fileUri(String path)
        {
            String fileRoot = bvConfiguration.getProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT);
            if (String.IsNullOrEmpty(fileRoot))
            {
                _logger.Error(BVMessageUtil.getMessage("ERR0010"));
                throw new BVSdkException("ERR0010");
            }

            String fullFilePath = Path.GetFullPath(fileRoot + "/" + path);
            
            Uri fileUri = new Uri(fullFilePath);
            return fileUri;
        }

        private Uri httpUri(String path)
        {
            Boolean isStaging = Boolean.Parse(bvConfiguration.getProperty(BVClientConfig.STAGING));
            Boolean isTesting = Boolean.Parse(bvConfiguration.getProperty(BVClientConfig.TESTING));
            Boolean isHttpsEnabled = Boolean.Parse(bvConfiguration.getProperty(BVClientConfig.SSL_ENABLED));
            String s3Hostname=null;
            String pathPrefix=null;

            if (
                bvParameters.SubjectType.getCS2013Text().Equals(
                    BVSubjectType.SELLER,
                    StringComparison.OrdinalIgnoreCase
                )
                &&
                bvParameters.ContentType.getContentType().Equals(
                    BVContentType.REVIEWS,
                    StringComparison.OrdinalIgnoreCase
                )
            )
            {
                s3Hostname = BVConstant.SELLER_RATINGS_S3_HOSTNAME;
                pathPrefix = "/";
                if (isTesting)
                {
                    pathPrefix += isStaging ?
                        BVConstant.ENVIRONMENT_TESTING_STAGING
                        :
                        BVConstant.ENVIRONMENT_TESTING;
                }
                else
                {
                    pathPrefix += isStaging ?
                        BVConstant.ENVIRONMENT_STAGING
                        :
                        BVConstant.ENVIRONMENT_PROD;
                }
            }
            else
            {
                pathPrefix = "";
                if (isTesting)
                {
                    s3Hostname = isStaging ?
                        bvConfiguration.getProperty(BVCoreConfig.TESTING_STAGING_S3_HOSTNAME)
                        :
                        bvConfiguration.getProperty(BVCoreConfig.TESTING_PRODUCTION_S3_HOSTNAME);
                }
                else
                {
                    s3Hostname = isStaging ?
                        bvConfiguration.getProperty(BVCoreConfig.STAGING_S3_HOSTNAME)
                        :
                        bvConfiguration.getProperty(BVCoreConfig.PRODUCTION_S3_HOSTNAME);
                }
            }
            
            String cloudKey = bvConfiguration.getProperty(BVClientConfig.CLOUD_KEY);
            String urlPath = pathPrefix + "/" + cloudKey + "/" + path;
            UriBuilder builder = new UriBuilder();
            builder.Scheme = isHttpsEnabled ? "https" : "http";
            if (s3Hostname.Contains(":"))
            {
                string[] parameters = s3Hostname.Split(':');
                foreach (string parameter in parameters)
                {
                    int portnum;
                    if (!int.TryParse(parameter, out portnum))
                    {
                        s3Hostname = parameter;
                    }
                    else
                    {
                        builder.Port = portnum;
                    }
                }
            }
            builder.Host = s3Hostname;
            builder.Path = urlPath;
            try
            {
                return builder.Uri;
            }
            catch (UriFormatException e)
            {
                _logger.Error(BVMessageUtil.getMessage("ERR0027"), e);
                throw new BVSdkException("ERR0027");
            }

            return null;
        }

        // bvpage is present here to have back compatability support if some sites
       	private Uri c2013Uri() 
        {
		    BVContentType contentType = null;
		    BVSubjectType subjectType = null;
		    String subjectId = null;

            NameValueCollection parameters = HttpUtility.ParseQueryString(_queryString, Encoding.UTF8);
		    for(int i=0; i < parameters.Count; i++ ) 
            {
                if (parameters.Keys[i] != null && parameters.Keys[i].Equals(BV_PAGE)) 
                {
                    string[] tokens = parameters[parameters.Keys[i]].Split('/');
            	    foreach(string token in tokens)
                    {
                        if (token.StartsWith("pg") && !IsValidPageNumber(bvParameters.PageNumber)) 
                        {
            			    bvParameters.PageNumber = getValue(token);
            		    } 
                        else if (token.StartsWith("ct") ) 
                        {
            			    contentType = new BVContentType(BVContentType.ctFromKeyWord(getValue(token)));
            		    } 
                        else if (token.StartsWith("st")) 
                        {
            			    subjectType = new BVSubjectType(BVSubjectType.subjectType(getValue(token)));
            		    } 
                        else if (token.StartsWith("id")) 
                        {
            			    subjectId = getValue(token);
            		    }
            	    }
                }
            }   

            contentType = (contentType == null) ? bvParameters.ContentType : contentType;
            subjectType = (subjectType == null) ? bvParameters.SubjectType : subjectType;
            subjectId = (String.IsNullOrEmpty(subjectId)) ? bvParameters.SubjectId : subjectId;

            if (!IsValidPageNumber(bvParameters.PageNumber))
                bvParameters.PageNumber = NUM_ONE_STR;

            String path = getPath(contentType, subjectType, bvParameters.PageNumber, subjectId, bvParameters.ContentSubType);
		    if (isContentFromFile()) {
			    return fileUri(path);
		    }
    		
		    return httpUri(path);
	    }

        private Uri bvstateUri(String queryString)
        {
            BVContentType contentType = null;
            BVSubjectType subjectType = null;
            String subjectId = null;
            String pageNumber = null;

            NameValueCollection parameters = HttpUtility.ParseQueryString(
                queryString,
                Encoding.UTF8
            );
            for (int i = 0; i < parameters.Count; i++)
            {
                if
                (
                    parameters.Keys[i] != null &&
                    parameters.Keys[i].Equals(BV_STATE)
                )
                {
                    string[] tokens = parameters[parameters.Keys[i]].Split(BVConstant.BVSTATE_TOKEN_SEPARATOR_CHAR);
                    foreach (string token in tokens)
                    {
                        if (token.StartsWith("pg"))
                        {
                            pageNumber = extractValue(token);
                        }
                        else if (token.StartsWith("ct"))
                        {
                            contentType = new BVContentType(
                                BVContentType.ctFromBVStateKeyword(
                                    extractValue(token)
                                )
                            );
                        }
                        else if (token.StartsWith("st"))
                        {
                            subjectType = new BVSubjectType(
                                extractValue(token)
                            );
                        }
                        else if (token.StartsWith("id"))
                        {
                            subjectId = extractValue(token);
                        }
                    }
                }
            }

            if
            (
                // Ignore bvstate if ContentType is Missing
                contentType == null ||
                // Ignore bvstate if contentType doesn't match bvParameters contentType
                (
                    bvParameters.ContentType != null &&
                    !contentType.getContentType().Equals(
                        bvParameters.ContentType.getContentType(),
                        StringComparison.OrdinalIgnoreCase
                    )
                )
            )
            {
                // when no uri is returned, it falls back to legacy seo parameters
                return null;
            }

            // Defaulting logic if no subjectType is provided
            if (subjectType == null)
            {
                if
                (
                    contentType.getContentType().Equals(
                        BVContentType.SPOTLIGHTS,
                        StringComparison.OrdinalIgnoreCase
                    )
                )
                {
                    subjectType = new BVSubjectType(BVSubjectType.CATEGORY);
                } 
                else 
                {
                    subjectType = new BVSubjectType(BVSubjectType.PRODUCT);
                }
            }
            subjectId = (String.IsNullOrEmpty(subjectId)) ? bvParameters.SubjectId : subjectId;
            bvParameters.SubjectId = subjectId;

            if (!IsValidPageNumber(pageNumber))
            {
                pageNumber = NUM_ONE_STR;
            }
            bvParameters.PageNumber = pageNumber;

            String path = getPath(
                contentType,
                subjectType,
                bvParameters.PageNumber,
                subjectId,
                bvParameters.ContentSubType
            );
            if (isContentFromFile())
            {
                return fileUri(path);
            }

            return httpUri(path);
        }

        
        private String getValue(String valueString)
        {
            return valueString.Substring(2, valueString.Length-2);
        }

        private String extractValue(String valueString)
        {
            if (valueString.Contains(BVConstant.BVSTATE_KEYVALUE_SEPARATOR_CHAR+""))
            {
                String[] data = valueString.Split(BVConstant.BVSTATE_KEYVALUE_SEPARATOR_CHAR);
                return data[1];
            }
            return "";
        }


        private String getPath(BVContentType contentType, BVSubjectType subjectType, String pageNumber, String subjectId, BVContentSubType contentSubType)
        {
            StringBuilder path = new StringBuilder();
            path.Append(getRootFolder());
            path.Append(PATH_SEPARATOR);
            path.Append(contentType.uriValue());
            path.Append(PATH_SEPARATOR);

            path.Append(subjectType.uriValue());
            path.Append(PATH_SEPARATOR);
            path.Append(pageNumber);
            path.Append(PATH_SEPARATOR);
            
            if (contentSubType != null && !contentSubType.getContentKeyword().Equals(BVContentSubType.NONE))
            {
                path.Append(contentSubType.getContentKeyword());
                path.Append(PATH_SEPARATOR);
            }

            path.Append(subjectId);
            path.Append(HTML_EXT);

            return path.ToString();
        }

        // return true if given string is a parseable integer > 0 and false otherwise
        private bool IsValidPageNumber(string pageNumber)
        {
            int result;
            bool success = Int32.TryParse(pageNumber, out result);

            if (success && result > 0)
                return true;
            else
                return false;
        }

        // if BVParameters.PageNumber isn't valid, check query string for page # (return default of "1")
        private void setPageNumber()
        {
            if (IsValidPageNumber(bvParameters.PageNumber))
                return;
            
            bvParameters.PageNumber = BVUtility.getPageNumber(queryString());
        }

        private String getRootFolder()
        {
            return bvConfiguration.getProperty(BVClientConfig.BV_ROOT_FOLDER);
        }

        private Boolean isContentFromFile()
        {
            Boolean loadFromFile = Boolean.Parse(bvConfiguration.getProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY));
            return loadFromFile;
        }

    }
}
