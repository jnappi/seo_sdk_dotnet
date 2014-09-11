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
using System.Diagnostics;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BVSeoSdkDotNet
{
    /**
    * Test class for testing Execution timeout settings
    */ 
    [TestClass]
    public class BVManagedUIContent_ExecutionTimeoutTest
    {
        /**
	    * Test case for user execution timeout implementation.
	    */
        [TestMethod]
        public void TestExecutionTimeout()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "agileville-78B2EF7DE83644CAB5F8C72F2D8C8491");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "2");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "NORMAL_USER OR Browser userAgent";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "data-gen-7k694zcnd6gbnpv2v4e6mmd22";

            String theUiContent = uiContent.getContent(bvParameters);
            Assert.IsTrue(theUiContent.Contains("getContent"), "there should be getContent word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: Execution timed out, exceeded"), 
                "there should be execution timeout message");

            uiContent = new BVManagedUIContent(bvConfig);
            theUiContent = uiContent.getReviews(bvParameters);
            Assert.IsTrue(theUiContent.Contains("getReviews"), "there should be getReviews word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: Execution timed out, exceeded"),
                "there should be execution timeout message");

            uiContent = new BVManagedUIContent(bvConfig);
            theUiContent = uiContent.getAggregateRating(bvParameters);
            Debug.WriteLine(theUiContent);
            Assert.IsTrue(theUiContent.Contains("getAggregateRating"), "there should be getAggregateRating word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: Execution timed out, exceeded"),
                "there should be execution timeout message");
        }

        /**
	    * Test case for user execution timeout when set to 0
	    */
        [TestMethod]
        public void estExecutionTimeout_ZeroTest()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "agileville-78B2EF7DE83644CAB5F8C72F2D8C8491");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "0");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "NORMAL_USER OR Browser userAgent";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "data-gen-7k694zcnd6gbnpv2v4e6mmd22";

            String theUiContent = uiContent.getContent(bvParameters);
            Assert.IsTrue(theUiContent.Contains("getContent"), "there should be getContent word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: EXECUTION_TIMEOUT is set to 0 ms; JavaScript-only Display."),
                "there should be execution timeout message");

            uiContent = new BVManagedUIContent(bvConfig);
            theUiContent = uiContent.getReviews(bvParameters);
            Assert.IsTrue(theUiContent.Contains("getReviews"), "there should be getReviews word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: EXECUTION_TIMEOUT is set to 0 ms; JavaScript-only Display."), 
                "there should be execution timeout message");

            uiContent = new BVManagedUIContent(bvConfig);
            theUiContent = uiContent.getAggregateRating(bvParameters);
            Debug.WriteLine(theUiContent);
            Assert.IsTrue(theUiContent.Contains("getAggregateRating"), "there should be getAggregateRating word/message");
            Assert.IsTrue(theUiContent.Contains("bvseo-msg: EXECUTION_TIMEOUT is set to 0 ms; JavaScript-only Display."),
                "there should be execution timeout message");
        }

        /**
	     * Test case for bot execution timeout implementation.
	     */
        [TestMethod]
        public void TestExecutionTimeoutBot()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "agileville-78B2EF7DE83644CAB5F8C72F2D8C8491");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT, "2");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "NORMAL_USER OR Browser userAgent";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "data-gen-7k694zcnd6gbnpv2v4e6mmd22";

            String theUiContent = uiContent.getContent(bvParameters);
            Assert.IsTrue(theUiContent.Contains("getContent"), "there should be getContent word/message");

            uiContent = new BVManagedUIContent(bvConfig);
            theUiContent = uiContent.getReviews(bvParameters);
            Debug.WriteLine(theUiContent);
            Assert.IsTrue(!theUiContent.Contains(
                    "bvseo-msg: EXECUTION_TIMEOUT_BOT is less than the minimum value allowed. Minimum value of 100ms used.;"),
                "there should be execution timeout message");

            uiContent = new BVManagedUIContent(bvConfig);
            bvParameters.UserAgent = "google";

            theUiContent = uiContent.getAggregateRating(bvParameters);
            Assert.IsTrue(theUiContent.Contains(
                    "bvseo-msg: EXECUTION_TIMEOUT_BOT is less than the minimum value allowed. Minimum value of 100ms used.;"),
                "there should be execution timeout message");
        }
    }
}