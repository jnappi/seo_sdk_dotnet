using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace BVSeoSdkDotNet
{
    /// <summary>
    /// Summary description for BVManagedUIContentTest
    /// </summary>
    [TestClass]
    public class BVManagedUIContentTest
    {
        public BVManagedUIContentTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;
        private BVConfiguration bvConfig;

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
        public void BVManagedUIContentTestInitialize() 
        {
            bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVCoreConfig.STAGING_S3_HOSTNAME, "google.com:81");
            bvConfig.addProperty(BVCoreConfig.PRODUCTION_S3_HOSTNAME, "google.com:81");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "rootFolder");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "cloudKey");
            bvConfig.addProperty(BVClientConfig.CONNECT_TIMEOUT, "100");
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, ".*(msnbot|google|teoma|bingbot|yandexbot|yahoo).*");
            bvConfig.addProperty(BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE, "false");
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");
            bvConfig.addProperty(BVClientConfig.SOCKET_TIMEOUT, "1000");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "30000");
        }

        //[TestMethod]
        //public void BVManagedUIContentTestConnectionTimeOut()
        //{
        //    BVParameters _bvParam = new BVParameters();
        //    _bvParam.UserAgent = "google";
        //    _bvParam.BaseURI = "http://localhost:8080/abcd/thispage.htm"; // this value is used to build pagination links
        //    _bvParam.PageURI = "http://localhost:8080/abcd/thispage.htm" + "?" + "notSure=1&letSee=2"; //this value is used to needed BV URL parameters
        //    _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
        //    _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
        //    _bvParam.SubjectId = "10204080000800000-I";

        //    BVUIContent bvUIContent = new BVManagedUIContent(bvConfig);
        //    String theContent = bvUIContent.getContent(_bvParam);
        //    Assert.AreEqual<Boolean>(theContent.Contains("timed out"), true, "There should timed out message.");
        //}

        [TestMethod]
        public void BVManagedUIContentTestExecutionTimeOut()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "2");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001";

            String theUIContent = uiContent.getContent(bvParameters);

            Assert.AreEqual<bool>(theUIContent.Contains("bvseo-msg: Execution timed out, exceeded"), true, "there should be execution timeout message");
        }

        [TestMethod]
        public void BVManagedUIContentTestPagination()
        {
            //Establish a new BVConfiguration.  Properties within this configuration are typically set in bvconfig.properties.
            //addProperty can be used to override configurations set in bvconfig.properties.
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch

            //this SDK supports retrieval of SEO contents from the cloud or local file system
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            _bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            //if LOAD_SEO_FILES_LOCALLY = false, configure CLOUD_KEY and STAGING
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY,"adobe-55d020998d7b4776fb0f9df49278083c"); // get this value from BV
            _bvConfig.addProperty(BVClientConfig.STAGING, "false");  // set to true for staging environment data
            _bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");

            //insert root folder with the value provided.
            //if multiple deployment zones/display codes are used for this implementation, use conditional logic to set the appropriate BV_ROOT_FOLDER
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814"); //get this value from BV
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");


            //Create BVParameters for each injection.  If the page contains multiple injections, for example //reviews and questions, set unique parameters for each injection.
            BVParameters _bvParam = new BVParameters();
            _bvParam.UserAgent = "yandex";
            _bvParam.BaseURI = "http://localhost:8080/Sample/Example-Adobe.jsp"; // this value is pagination links
            _bvParam.PageURI = "http://localhost:8080/Sample/Example-Adobe.jsp?bvrrp=8814/reviews/product/4/PR6.htm&bvreveal=debug"; //this should be the current page, full URL
            _bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            _bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            _bvParam.SubjectId = "PR6";

            BVUIContent _bvOutput = new BVManagedUIContent(_bvConfig);

            String sBvOutputReviews = _bvOutput.getContent(_bvParam); //String sBvOutputSummary = _bvOutput.getAggregateRating(_bvParam);
            Assert.AreEqual<Boolean>(sBvOutputReviews.Contains("BVRRSelectedPageNumber\">4<"), true, "there should be BVRRSelectedPageNumber as 4 in the content");
        }

        [TestMethod]
        public void TestSearchContentNullBVQueryParams()
        {
            BVUIContent bvUIContent = new BVManagedUIContent();
		    String bvContent = null;
		
		    bvContent = bvUIContent.getContent(null);
			Assert.IsTrue(bvContent.Contains("<li id=\"ms\">bvseo-msg: BVParameters is null.;</li>"), "Message are not same please verify.");
        }

        //[TestMethod]
        //public void TestIncludeIntegrationScript()
        //{
        //    bvConfig = new BVSdkConfiguration();
        //    bvConfig.addProperty(BVClientConfig.BOT_DETECTION, "True");
        //    bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "12325");
        //    bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "afgedbd");
        //    bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
        //    bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/seo_local_files");
        //    bvConfig.addProperty(BVClientConfig.STAGING, "True");
        //}
    }
}
