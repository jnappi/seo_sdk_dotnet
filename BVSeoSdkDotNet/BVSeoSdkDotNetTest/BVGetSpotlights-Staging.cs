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
    public class BVGetSpotlightsStaging
    {
        private BVUIContent _bvUIContent;

        private const String CLOUD_KEY = "spotlight-four-746e2fc1211dc8964560350c9f28b67a";
        private const String DISPLAY_CODE = "Main_Site-en_US";
        private const long CONTENT_RETRIEVAL_THRESHOLD = 1500;
        private Stopwatch stopwatch = new Stopwatch();

        public BVGetSpotlightsStaging()
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
        public void BVGetSpotlights_TestInitialize()
        {
            BVConfiguration _bvConfiguration = new BVSdkConfiguration();
            _bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, DISPLAY_CODE);
            _bvConfiguration.addProperty(BVClientConfig.CLOUD_KEY, CLOUD_KEY);
            _bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            _bvConfiguration.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            _bvConfiguration.addProperty(BVClientConfig.STAGING, "true");
            _bvConfiguration.addProperty(BVClientConfig.TESTING, "false");
            _bvConfiguration.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");
            

            _bvUIContent = new BVManagedUIContent(_bvConfiguration);
        }

        [TestMethod]
        public void Test_Spotlights_Staging()
        {
            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.SPOTLIGHTS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.CATEGORY);
            
            bvParameters.SubjectId = "category-3";
            bvParameters.BaseURI = "http://www.example.com/store/products/reviews";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/?utm_campaign=bazaarvoice&utm_medium=SearchVoice&utm_source=RatingsAndReviews&utm_content=Default&bvrrp=Main_Site-en_US/reviews/product/1/50524.htm";
            bvParameters.PageNumber = "1";
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
            Console.WriteLine(content);
            Assert.IsTrue(stopwatch.ElapsedMilliseconds <= CONTENT_RETRIEVAL_THRESHOLD, String.Format("Content retrieval time exceeded {0} milliseconds", CONTENT_RETRIEVAL_THRESHOLD));
            Assert.IsTrue(!content.Contains ("<li data-bvseo=\"ms\">bvseo-msg:"));
            Assert.IsTrue(content.Contains("<li data-bvseo=\"ct_st\">SPOTLIGHTS"));
            Assert.IsFalse(content.Contains("HTTP 403 Forbidden"), "There should be valid content");
        }


       
    }
}
