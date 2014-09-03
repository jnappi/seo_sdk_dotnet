using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVManagedUIContentAggregateReviewTest
    /// </summary>
    [TestClass]
    public class BVManagedUIContentAggregateReviewTest
    {
        public BVManagedUIContentAggregateReviewTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePagePRR_AggregateRating()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "3000");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "2000002";

            String theUIContent = uiContent.getAggregateRating(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should be BvRRSourceID in the content");
            Assert.AreEqual<Boolean>(!theUIContent.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should not be reviews section in the content");
        }

        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePagePRR_Reviews()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "3000");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "2000002";

            String theUIContent = uiContent.getReviews(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should be reviews section in the content");
            Assert.AreEqual<Boolean>(!theUIContent.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should not be AggregateRating in the content");
            
        }

        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_AggregateRating_BotDetectionEnabled()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            _bvConfig.addProperty(BVClientConfig.STAGING, "true");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "3000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_3) AppleWebKit/536.29.13 (KHTML, like Gecko) Version/6.0.4 Safari/536.29.13";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm"; 
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "2000002";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should not be AggregateRating in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true, 
				"there should not be reviews section in the content");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("JavaScript-only Display"), true, "There should be JavaScript display element");
		
		    String sBvOutputReviews = _bvOutput.getReviews(_bvParam);
            Assert.AreEqual<Boolean>(!sBvOutputReviews.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true, 
				    "there should not be reviews section in the content");
            Assert.AreEqual<Boolean>(!sBvOutputReviews.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                    true, "there should not be AggregateRating in the content");
            Assert.AreEqual<Boolean>(sBvOutputReviews.Contains("JavaScript-only Display"), true, "There should be JavaScript display element");
        }


        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_AggregateRatingAndReviews()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            _bvConfig.addProperty(BVClientConfig.STAGING, "true");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "3000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "googlebot";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "2000001";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should be AggregateRating in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                "there should not be reviews section in the content");

            String sBvOutputReviews = _bvOutput.getReviews(_bvParam);
            Assert.AreEqual<Boolean>(sBvOutputReviews.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should be reviews section in the content");
            Assert.AreEqual<Boolean>(!sBvOutputReviews.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                    true, "there should not be AggregateRating in the content");
        }


        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_GetReviews_ERR0013()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-55d020998d7b4776fb0f9df49278083c");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "googlebot";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "PR6";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should be AggregateRating in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                "there should not be reviews section in the content");

            String sBvOutputReviews = _bvOutput.getReviews(_bvParam);
            Assert.AreEqual<Boolean>(sBvOutputReviews.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should be reviews section in the content");
            Assert.AreEqual<Boolean>(!sBvOutputReviews.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                    true, "there should not be AggregateRating in the content");
        }

        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_GetReviews_Blank_Page_Test()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "seo_sdk_testcase-159b6108bb11967e554a92c6a3c39cb3");
            _bvConfig.addProperty(BVClientConfig.STAGING, "true");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "googlebot";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "5000002_NO_BV";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getReviews(_bvParam);

            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should not be reviews section in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                true, "there should not be AggregateRating in the content");
            String expectedMessage = BVMessageUtil.getMessage("ERR0012");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains(expectedMessage),
                true, "Message does not contain expected message please test");
        }

        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_AggregateRating_IfNotPresent()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "seo_sdk_testcase-159b6108bb11967e554a92c6a3c39cb3");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "5000002_No_Aggr_Rating";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\""),
                    true, "there should not be AggregateRating in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should not be reviews section in the content");
            String expectedMessage = BVMessageUtil.getMessage("ERR0012");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains(expectedMessage),
                true, "Message does not contain expected message please test");
        }

        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_Aggregate_WithoutPagination()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-55d020998d7b4776fb0f9df49278083c");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "PR6";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<!--begin-aggregate-rating-->"), true, "there should be AggregateRating in the content");
            Assert.AreEqual<Boolean>(!sBvOutputSummary.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\""), true,
                    "there should not be reviews section in the content");
        }

        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_GetContent_SDK_Disabled()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "false");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "hartz-2605f8e4ef6790962627644cc195acf2");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "11568-en_US");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "1577";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getContent(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<li id=\"en\">bvseo-false</li>"), true, "There should only footer message");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("BVRRReviewsSoiSectionID"), false, "There should not be any reviews");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<span itemprop=\"aggregateRating\" itemscope "), false, "There should not be any ratings");

        }


        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_GetAggregateRating_SDK_Disabled()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "false");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "hartz-2605f8e4ef6790962627644cc195acf2");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "11568-en_US");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "1577";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<li id=\"en\">bvseo-false</li>"), true, "There should only footer message");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<!--begin-aggregate-rating-->"), false, "there should not be aggregateRating in the content");
        }


        [TestMethod]
        public void TestSEOContent_SinglePageHTTP_GetReview_SDK_Disabled()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "false");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "hartz-2605f8e4ef6790962627644cc195acf2");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "11568-en_US");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "1577";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getReviews(_bvParam);

            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("<li id=\"en\">bvseo-false</li>"), true, "There should only footer message");
            Assert.AreEqual<Boolean>(sBvOutputSummary.Contains("BVRRReviewsSoiSectionID"), false, "There should not be any reviews");
        }

        /// <summary>
        /// Crawler patter change as user will enter any pattern and will be straight
        /// string. if they are using separator,  they will be using '|' to separte words.
        /// </summary>
        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePagePRR_CrawlerOverride()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-55d020998d7b4776fb0f9df49278083c");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "mysearchbot");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "111mysearchbot122";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "PR6";

            String theUIContent = uiContent.getAggregateRating(bvParameters);
            Assert.IsTrue(theUIContent.Contains("<span itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\">"),
                 "there should be BvRRSourceID in the content");
            Assert.IsFalse(theUIContent.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\">"),
                "there should not be reviews section in the content");

            /** Scenario for multiple crawler patterh **/
            bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-55d020998d7b4776fb0f9df49278083c");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "mysearchbot|anotherbot");

            uiContent = new BVManagedUIContent(bvConfig);

            bvParameters = new BVParameters();
            bvParameters.UserAgent = "111mysearchbot122";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "PR6";

            theUIContent = uiContent.getAggregateRating(bvParameters);
            Assert.IsTrue(theUIContent.Contains("<span itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\">"),
                 "there should be BvRRSourceID in the content");
            Assert.IsFalse(theUIContent.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\">"),
                "there should not be reviews section in the content");

            bvParameters = new BVParameters();
            bvParameters.UserAgent = "111anotherbot122";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "PR6";

            theUIContent = uiContent.getAggregateRating(bvParameters);
            Assert.IsTrue(theUIContent.Contains("<span itemprop=\"aggregateRating\" itemscope itemtype=\"http://schema.org/AggregateRating\">"),
                 "there should be BvRRSourceID in the content");
            Assert.IsFalse(theUIContent.Contains("itemprop=\"review\" itemscope itemtype=\"http://schema.org/Review\">"),
                "there should not be reviews section in the content");
        }


        /// <summary>
        /// When null subject id was set, there was no proper error message displayed since the refactoring greating affected.
        /// </summary>
        [TestMethod]
        public void TestAggregate_NullSubjectID()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "hartz-2605f8e4ef6790962627644cc195acf2");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "11568-en_US");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = null;

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: SubjectId cannot be null or empty.;</li>"), 
						"there should be error message for SubjectId");

            sBvOutputSummary = _bvOutput.getReviews(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: SubjectId cannot be null or empty.;</li>"),
                        "there should be error message for SubjectId");

            sBvOutputSummary = _bvOutput.getContent(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: SubjectId cannot be null or empty.;</li>"),
                        "there should be error message for SubjectId");
        }

        /// <summary>
        /// When Cloud key is invalid.
        /// </summary>
        [TestMethod]
        public void TestAggregate_InvalidURL()
        {
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-invalid-55d020998d7b4776fb0f9df49278083c");
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814");
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "google";
            _bvParam.BaseURI = "http://localhost:8080/thispage.htm";
            _bvParam.PageURI = "http://localhost:8080/abcd" + "?" + "notSure=1&letSee=2";
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "PR6";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: The resource to the URL or file is currently unavailable.;</li>"),
                        "there should be error message for SubjectId");

            sBvOutputSummary = _bvOutput.getReviews(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: The resource to the URL or file is currently unavailable.;</li>"),
                        "there should be error message for SubjectId");

            sBvOutputSummary = _bvOutput.getContent(_bvParam);
            Assert.IsTrue(sBvOutputSummary.Contains("<li id=\"ms\">bvseo-msg: The resource to the URL or file is currently unavailable.;</li>"),
                        "there should be error message for SubjectId");
        }
    }
}
