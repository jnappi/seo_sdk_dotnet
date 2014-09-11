using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.BVException;
using System.Diagnostics;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class BVManagedUIContent2_Test
    {
        private BVUIContent _bvUIContent;

        private const String CLOUD_KEY = "bodyglove-8e186f6e16e2d688784728b360df41c5";
        private const String DISPLAY_CODE = "Main_Site-en_US";
        private const long CONTENT_RETRIEVAL_THRESHOLD = 1500;
        private Stopwatch stopwatch = new Stopwatch();

        public BVManagedUIContent2_Test()
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

        [TestInitialize()]
        public void BVManagedUIContent2_TestInitialize()
        {
            BVConfiguration _bvConfiguration = new BVSdkConfiguration();
            _bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, DISPLAY_CODE);
            _bvConfiguration.addProperty(BVClientConfig.CLOUD_KEY, CLOUD_KEY);
            _bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfiguration.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfiguration.addProperty(BVClientConfig.STAGING, "true");
            _bvConfiguration.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            _bvUIContent = new BVManagedUIContent(_bvConfiguration);
        }

        [TestMethod]
        public void Test_InteractivePage_Review()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/reviews";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/reviews/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
            try
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e)
            {
                erroMessage = e.getMessage();
            }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
            Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }

        [TestMethod]
        public void Test_InteractivePage_Question() 
        {
		    BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.QUESTIONS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/question";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/questions/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
		    try 
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e) 
            {
			    erroMessage = e.getMessage();
		    }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
		    Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
		    Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
	    }


        [TestMethod]
        public void Test_InteractivePage_Stories()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.STORIES);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/story";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/story/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
            try
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e)
            {
                erroMessage = e.getMessage();
            }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
            Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }


        [TestMethod]
        public void Test_SinglePage_Review()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/reviews";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/reviews/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
            try
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e)
            {
                erroMessage = e.getMessage();
            }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
            Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }


        [TestMethod]
        public void Test_SinglePage_Question()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.QUESTIONS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/question";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/questions/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
            try
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e)
            {
                erroMessage = e.getMessage();
            }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
            Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }


        [TestMethod]
        public void Test_SinglePage_Stories()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.STORIES);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            bvParameters.BaseURI = "http://www.example.com/store/products/story";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/story/product/2/50524.htm";
            String erroMessage = null;
            String content = null;
            try
            {
                stopwatch.Restart();
                content = _bvUIContent.getContent(bvParameters);
                stopwatch.Stop();
            }
            catch (BVSdkException e)
            {
                erroMessage = e.getMessage();
            }

            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsNull(erroMessage, "There should not be any errorMessage");
            Assert.IsNotNull(content, "There should be content to proceed further assertion!!");
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }

        [TestMethod]
        public void Test_PageNumber_C2013()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfiguration.addProperty(BVClientConfig.CLOUD_KEY, "agileville-78B2EF7DE83644CAB5F8C72F2D8C8491");
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfiguration.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            bvConfiguration.addProperty(BVClientConfig.STAGING, "true");

            BVUIContent bvUIContent = new BVManagedUIContent(bvConfiguration);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.PageNumber = "2";
            bvParameters.BaseURI = "http://www.example.com/store/products/reviews";
            bvParameters.PageURI =
                "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvpage=pg3/ctre/stp/iddata-gen-5zkafmln4wymhcfbp5u6hmv5q&bvreveal=debug";

            String content = bvUIContent.getContent(bvParameters);

            Assert.IsTrue(
                content.Contains("http://seo-stg.bazaarvoice.com/agileville-78B2EF7DE83644CAB5F8C72F2D8C8491/"
                                 + "Main_Site-en_US/reviews/product/2/data-gen-5zkafmln4wymhcfbp5u6hmv5q.htm"),
                "URL should be valid url");
            Assert.IsTrue(!content.Contains("The resource to the URL or file is currently unavailable"),
                "resource not found or unavailable message should not be there");

            Debug.WriteLine(content);
        }
    }
}
