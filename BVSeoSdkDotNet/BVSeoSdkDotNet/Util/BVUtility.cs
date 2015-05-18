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
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Reflection;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.BVException;
using log4net;

namespace BVSeoSdkDotNet.Util
{
    /// <summary>
    /// Bazaarvoice utility class.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public sealed class BVUtility
    {
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Regex BV_PATTERN = null;
        private static readonly IList<String> bvQueryParameters = new ReadOnlyCollection<string>(
            new List<String> {
                "bvrrp",
                "bvsyp",
                "bvqap",
                "bvpage"
            }
        );
        private const String ESCAPED_FRAGMENT = "_escaped_fragment_";

        /// <summary>
        /// Gets the Page Number from the Page URI QueryString 
        /// </summary>
        /// <param name="queryString">QueryString from the PageURI</param>
        /// <returns>Page Numer as String</returns>
        public static String getPageNumber(String queryString) 
        {
            if (queryString != null && queryString.Length > 0) 
            {
                NameValueCollection parameters = HttpUtility.ParseQueryString(queryString, Encoding.UTF8);

                for (int i = 0; i < parameters.Count; i++)
                {
                    if (parameters.Keys[i] != null && (parameters.Keys[i].Equals("bvrrp") || parameters.Keys[i].Equals("bvqap")))
                    {
                        Regex p = new Regex("^[^/]+/\\w+/\\w+/(\\d+)/[^/]+\\.htm$", RegexOptions.Compiled);
                        return matchPageNumber(p, parameters[parameters.Keys[i]]);
                    }
                    else if (parameters.Keys[i] != null && parameters.Keys[i].Equals("bvsyp"))
                    {
                        Regex p = new Regex("^[^/]+/\\w+/\\w+/(\\d+)[[/\\w+]|[^/]]+\\.htm$", RegexOptions.Compiled);
                        return matchPageNumber(p, parameters[parameters.Keys[i]]);
                    }
                    else if (parameters.Keys[i] != null && parameters.Keys[i].Equals("bvpage"))
                    {
                        Regex p = new Regex("^\\w+/(\\d+)$", RegexOptions.Compiled);
                        return matchPageNumber(p, parameters[parameters.Keys[i]]);
                    }
                }
            }
            return "1";
        }

        private static String matchPageNumber(Regex pattern, String value)
        {
            Match m = pattern.Match(value);
            if (m.Success && m.Groups != null && m.Groups.Count == 2 && m.Groups[1] != null)
            {
                return m.Groups[1].Value;
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// Get the Query String from the URI
        /// </summary>
        /// <param name="uri">URI as String</param>
        /// <returns>Query String</returns>
        public static String getQueryString(String uri)
        {
            if (String.IsNullOrEmpty(uri))
            {
                return "";
            }

            Uri _uri = null;
            try
            {
                _uri = new Uri(uri);
            }
            catch (Exception ex)
            {
                _logger.Error(BVMessageUtil.getMessage("ERR0027"),ex);
                throw new BVSdkException("ERR0027");
            }
            return _uri.Query;
        }

        /// <summary>
        /// Get the Fragment String from the URI
        /// </summary>
        /// <param name="uri">URI as String</param>
        /// <returns>Fragment String</returns>
        public static String getFragmentString(String uri)
        {
            if (String.IsNullOrEmpty(uri))
            {
                return "";
            }

            Uri _uri = null;
            try
            {
                _uri = new Uri(uri);
            }
            catch (Exception ex)
            {
                _logger.Error(BVMessageUtil.getMessage("ERR0027"), ex);
                throw new BVSdkException("ERR0027");
            }
            return _uri.Fragment;
        }
     
        /// <summary>
        /// Read a contents of a File from the Path specified and return the contents as a string
        /// </summary>
        /// <param name="path">File Path as String</param>
        /// <returns>File Contents as String</returns>
   	    public static String readFile(String path) 
        {
            String decodedString = "";
            if(!File.Exists(path))
            {
                _logger.Error(BVMessageUtil.getMessage("ERR0012") + " : File Doesn't Exist");
                throw new BVSdkException("ERR0012");
            }
            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                byte[] buffer = new byte[16*1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    try
                    {
                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        decodedString = Encoding.UTF8.GetString(ms.ToArray());
                    }
                    catch(Exception e)
                    {
                        _logger.Error(e.Message, e);
                    }
                    finally
                    {
                        ms.Close();
                        stream.Close();
                    }
                }
                return decodedString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return ex.Message;
            }
        }

        public static void replacePageURIFromContent(StringBuilder content, String baseUri)
        {
            String toAppendToBaseUri = "";
            // baseUri has hashbang or a non _escaped_fragment_ url
            if
            (
                baseUri.Contains(BVConstant.FRAGMENT_MARKER) ||
                !baseUri.Contains(BVConstant.ESCAPED_FRAGMENT_KEY)
            )
            {
                // If baseUri ends with #! or ? then no need to append anything
                // Rest of cases, we need to append & when ? is available else ?
                if
                (
                    !baseUri.EndsWith(BVConstant.FRAGMENT_MARKER) &&
                    !baseUri.EndsWith("?")
                )
                {
                    toAppendToBaseUri = (baseUri.Contains("?") ? "&" : "?");
                }
            }
            // base uri is an _escaped_fragment_ url
            else
            {
                // if baseUri ends with _escaped_fragment_ no need to append anything
                // Rest of cases, we need to append %26 when ? is available else ?
                if (!baseUri.EndsWith(BVConstant.ESCAPED_FRAGMENT_KEY))
                {
                    String escapedFragmentValue = Regex.Split(
                        baseUri,
                        BVConstant.ESCAPED_FRAGMENT_KEY
                    )[1];
                    toAppendToBaseUri = (escapedFragmentValue.Contains("?") ? BVConstant.ESCAPED_FRAGMENT_MULTIVALUE_SEPERATOR : "?");
                }
            }

            BVUtility.replaceString(
                content,
                BVConstant.INCLUDE_PAGE_URI, baseUri + toAppendToBaseUri
            );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="toReplace"></param>
        /// <param name="replacement"></param>
        public static void replaceString(StringBuilder sb, String toReplace, String replacement)
        {
            sb.Replace(toReplace, replacement);
        }

        /// <summary>
        /// Removes the BV parameter values from the Url and returns the modified Url
        /// </summary>
        /// <param name="baseUrl">URL as String</param>
        /// <returns>URL as String</returns>
        public static String removeBVParameters(String url) 
        {
            String baseUrl = url;
            if (baseUrl.Contains("bvstate="))
            {
                // This handles case where there are more parameters after bvstate
                String regexStr = BVConstant.BVSTATE_REGEX_WITH_TRAILING_SEPERATOR;
                baseUrl = Regex.Replace(baseUrl, regexStr, "");

                // Handle usecase where bvstate is the last parameter
                if (baseUrl.EndsWith("?") | baseUrl.EndsWith("&"))
                {
                    baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
                }
                // Handle usecase where bvstate is the last parameter in _escaped_fragment_
                if (baseUrl.EndsWith(BVConstant.ESCAPED_FRAGMENT_MULTIVALUE_SEPERATOR))
                {
                    baseUrl = baseUrl.Substring(0, baseUrl.Length - 3);
                }
            }

            if (hasBVQueryParameters(baseUrl))
            {
                Uri uri;
                try
                {
                    uri = new Uri(baseUrl);
                    String newQuery = null;
                    if (uri.Query != null && uri.Query.Length > 0)
                    {
                        NameValueCollection newParameters = new NameValueCollection();
                        NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query, Encoding.UTF8);

                        foreach (string key in parameters.AllKeys)
                        {
                            if (!bvQueryParameters.Contains(key))
                            {
                                newParameters.Add(key, parameters[key]);
                            }
                        }
                        newQuery = newParameters.Count > 0 ? ConstructQueryString(newParameters, "&") : null;
                    }
                    UriBuilder uriBuilder = new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath);
                    uriBuilder.Query = newQuery;
                    return uriBuilder.Uri.ToString();
                }
                // swallow exceptions
                catch (Exception e) {
                    _logger.Info(e.Message, e);
                }
            }
            return baseUrl;
        }

        /// <summary>
        /// Constructs a QueryString (string).
        /// Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"
        /// </summary>
        /// <param name="parameters">NameValueCollection</param>
        /// <param name="joinString">String</param>
        /// <returns>String</returns>
        public static string ConstructQueryString(
            NameValueCollection parameters,
            String joinString
        )
        {
            List<string> items = new List<string>();

            foreach (String name in parameters)
            {
                if (name != null && name.Length > 0)
                {
                    items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name])));
                }
                else
                {
                    items.Add(System.Web.HttpUtility.UrlEncode(parameters[name]));
                }
            }

            return String.Join(joinString, items.ToArray());
        }

        /// <summary>
        /// Validates if the content has a valid bv content.
        /// This is used to validate if the response is not corrupted.
        /// </summary>
        /// <param name="content">Content to validate as String</param>
        /// <returns>bool value indicating the validity</returns>
        public static bool validateBVContent(String content)
        {
            if (BV_PATTERN == null)
            {
                BV_PATTERN = new Regex(BVConstant.BV_STRING_PATTERN, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            Match m = BV_PATTERN.Match(content);
            return m.Success;
        }

        /// <summary>
        /// Checks bvParameters to see whether we need to reveal debug information.
        /// </summary>
        /// <param name="bvParameters"></param>
        /// <returns>true when we have to reveal debug information, false otherwise</returns>
        public static Boolean isRevealDebugEnabled(BVParameters bvParameters)
        {   
            Regex regex = new Regex(BVConstant.BVSTATE_REVEAL_DEBUG_REGEX);
            return
            (
                bvParameters != null && 
                bvParameters.PageURI != null &&
                (
                    // check for presence of bvreveal=debug
                    bvParameters.PageURI.Contains(BVConstant.BVREVEAL_DEBUG) ||
                    // check for reveal:debug in bvstate
                    regex.Match(bvParameters.PageURI).Success
                )
            );
        }

        private static Boolean hasBVQueryParameters(String queryUrl)
        {
            foreach (String bvParameter in bvQueryParameters)
            {
                if (queryUrl.Contains(bvParameter))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
