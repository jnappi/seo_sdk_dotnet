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
	    private const String NUM_ONE_STR = "1";
	    private const String HTML_EXT = ".htm";
        private const String PATH_SEPARATOR = "/";

        private BVConfiguration bvConfiguration;
        private BVParameters bvParameters;
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
            String baseUri = bvParameters.BaseURI == null ? "" : bvParameters.BaseURI;

            if (baseUri.Contains("bvrrp") || baseUri.Contains("bvqap") ||
                    baseUri.Contains("bvsyp") || baseUri.Contains("bvpage"))
            {
                baseUri = BVUtilty.removeBVQuery(baseUri);
            }

            return baseUri;
        }

        /// <summary>
        /// Retrieves the query string portion from the URL.
        /// </summary>
        /// <returns>Returns the Query String</returns>
        public String queryString()
        {
            if (this._queryString == null)
            {
                this._queryString = BVUtilty.getQueryString(bvParameters.PageURI);
            }
            return this._queryString;
        }

        /// <summary>
        /// Forms either an http URL or file URL to access the content.
        /// </summary>
        /// <returns>Returns the URL in URI format</returns>
        public Uri seoContentUri()
        {
            if (_queryString != null && _queryString.Contains(BV_PAGE))
            {
                return c2013Uri();
            }

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
            String s3Hostname = isTesting ? bvConfiguration.getProperty(BVCoreConfig.TESTING_S3_HOSTNAME) : isStaging ? bvConfiguration.getProperty(BVCoreConfig.STAGING_S3_HOSTNAME) :
                bvConfiguration.getProperty(BVCoreConfig.PRODUCTION_S3_HOSTNAME);
            
            String cloudKey = bvConfiguration.getProperty(BVClientConfig.CLOUD_KEY);
            String urlPath = "/" + cloudKey + "/" + path;
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

        //need to change this method to handle the changes for BVState
        //Future FIXME
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

            contentType = (contentType == null)  ? bvParameters.ContentType : contentType;
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


        
        private String getValue(String valueString)
        {
            return valueString.Substring(2, valueString.Length-2);
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
            
            bvParameters.PageNumber = BVUtilty.getPageNumber(queryString());
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
