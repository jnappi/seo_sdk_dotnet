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
using BVSeoSdkDotNet.BVException;

namespace BVSeoSdkDotNet.Util
{
    /// <summary>
    /// Bazaarvoice utility class.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    public sealed class BVUtilty
    {
        private static Regex BV_PATTERN = null;

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
                for(int i=0; i < parameters.Count; i++ ) 
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
                throw new BVSdkException("ERR0026");
            }

            return _uri.Query;
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
                return "File Doesn't Exist";
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
                return ex.Message;
            }
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
        /// Removes the Bazaar Voice Query sring values from the Urls Query string and returns the modified Url
        /// </summary>
        /// <param name="queryUrl">Query URL as String</param>
        /// <returns>URL as String</returns>
        public static String removeBVQuery(String queryUrl) 
        {
            Uri uri;
            try 
            {
                uri = new Uri(queryUrl);
            } 
            catch (Exception e) 
            {
                return queryUrl;
            }

            try 
            {
                String newQuery = null;
                if (uri.Query != null && uri.Query.Length > 0) 
                {
                    NameValueCollection newParameters = new NameValueCollection();
                    NameValueCollection parameters = HttpUtility.ParseQueryString(uri.Query, Encoding.UTF8);
                    ArrayList  bvParameters = new ArrayList ();
                    bvParameters.Add("bvrrp");
                    bvParameters.Add("bvsyp");
                    bvParameters.Add("bvqap");
                    bvParameters.Add("bvpage");
                    foreach(KeyValuePair<string,string> parameter in parameters) 
                    {
                        if (!bvParameters.Contains(parameter.Key)) {
                            newParameters.Add(parameter.Key, parameter.Value);
                        }
                    }
                    newQuery = newParameters.Count > 0 ? ConstructQueryString(newParameters): null;
                }
                UriBuilder uriBuilder = new UriBuilder(uri.Scheme, uri.Host,uri.Port,uri.AbsolutePath);
                uriBuilder.Query = newQuery;
                return uriBuilder.Uri.ToString();
            } 
            catch (Exception e) 
            {
                return queryUrl;
            }
        }

        /// <summary>
        /// Constructs a QueryString (string).
        /// Consider this method to be the opposite of "System.Web.HttpUtility.ParseQueryString"
        /// </summary>
        /// <param name="parameters">NameValueCollection</param>
        /// <returns>String</returns>
        public static string ConstructQueryString(NameValueCollection parameters)
        {
            List<string> items = new List<string>();

            foreach (String name in parameters)
                items.Add(String.Concat(name, "=", System.Web.HttpUtility.UrlEncode(parameters[name])));

            return String.Join("&", items.ToArray());
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
    }
}
