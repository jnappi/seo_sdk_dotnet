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
    /// Summary description for BVManagedUIContent_SinglePageTest
    /// </summary>
    [TestClass]
    public class BVManagedUIContent_SinglePageTest
    {
        public BVManagedUIContent_SinglePageTest()
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
        public void TestSEOContentFromFile_SinglePagePRR()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, @"C:\Test\seo_local_files\myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("BVRRSourceID"), true, "there should be BvRRSourceID in the content");
        }

        [TestMethod]
        public void TestSEOContentFromFile_SinglePagePRR_PageUri()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, @"C:\Test\seo_local_files\myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001";
            bvParameters.PageURI = "http://localhost:8080/sample/xyz.jsp";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("BVRRSourceID"), true, "there should be BvRRSourceID in the content");
        }

        [TestMethod]
        public void TestSEOContentFromFile_SinglePageC2013()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, @"C:\Test\seo_local_files\myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.PageURI = "http://localhost:8080/sample/someproduct.jsp?bvpage=ctre/id3000001/stp";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("BVRRSourceID"), true, "there should be BvRRSourceID in the content");
        }

        [TestMethod]
        public void TestSEOContentFromFile_SinglePage_FileUnavailableError()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, @"E:\alpha-beta-invalid-\Seo_cyberduck\myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001_thisFiledoesNotExist";
            
            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(!theUIContent.Contains("BVRRSourceID"), true, "there should not be BvRRSourceID in the content");
            Assert.AreEqual<Boolean>(theUIContent.Contains("The resource to the URL or file is currently unavailable."), true, "there should be resource unavailable message");
        }

        [TestMethod]
        public void TestSEOContentFromFile_SinglePage_FileUnavailableError_bvReveal()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "True");
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, @"E:\alpha-beta-invalid-\Seo_cyberduck\myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344seob");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001_thisFiledoesNotExist";
            bvParameters.BaseURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/";
            bvParameters.PageURI = "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2?bvreveal=debug";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(!theUIContent.Contains("BVRRSourceID"), true, "there should not be BvRRSourceID in the content");
            Assert.AreEqual<Boolean>(theUIContent.Contains("The resource to the URL or file is currently unavailable."), true, "there should be resource unavailable message");
            Assert.AreEqual<Boolean>(theUIContent.Contains("debug"), true, "there should be debug message");
        }
        
        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePagePRR()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";
            
            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("bvseo-reviewsSection"), true, "there should be bvseo-reviewsSection in the content");
        }

        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePageC2013()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.PageURI = "http://localhost:8080/sample/someproduct.jsp?bvpage=ctre/id50524/stp";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(theUIContent.Contains("bvseo-reviewsSection"), true, "there should be bvseo-reviewsSection in the content");
        }

        [TestMethod]
        public void TestSEOContentFromHTTP_SinglePage_FileUnavailableError()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "3000001_thisFiledoesNotExist";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(!theUIContent.Contains("bvseo-reviewsSection"), true, "there should not be bvseo-reviewsSection in the content");
            Assert.AreEqual<Boolean>(theUIContent.Contains("The resource to the URL or file is currently unavailable."), true, "there should be resource unavailable message");
        }


        [TestMethod]
        public void TestSEOContentFromHTTP__InvalidCloudKey()
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove_bad_URL-8e186f6e16e2d688784728b360df41c5");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            BVParameters bvParameters = new BVParameters();
            bvParameters.UserAgent = "google";
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.SubjectId = "50524";

            String theUIContent = uiContent.getContent(bvParameters);
            Assert.AreEqual<Boolean>(!theUIContent.Contains("bvseo-reviewsSection"), true, "there should not be bvseo-reviewsSection in the content");
            Assert.AreEqual<Boolean>(theUIContent.Contains("The resource to the URL or file is currently unavailable."), true, "there should be resource unavailable message");
        }
    }
}
