using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.Url;
using BVSeoSdkDotNet.BVException;
using BVSeoSdkDotNet.Footer;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVHTMLFooterTest
    /// </summary>
    [TestClass]
    public class BVHTMLFooterTest
    {
        public BVHTMLFooterTest()
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
        public void TestDisplayFooter()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            BVParameters bvParameters = new BVParameters();
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            BVFooter bvFooter = new BVHTMLFooter(bvConfiguration, bvParameters);

            String displayFooter = bvFooter.displayFooter("getContent");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"mt\">bvseo-CLOUD</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"ct\">bvseo-${_bvParameters.ContentType.uriValue()}</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"st\">bvseo-product</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=seo.sdk.execution.timeout>"), false, "the content string should not match.");
        }

        [TestMethod]
        public void TestDisplayFooter_Debug()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParameters = new BVParameters();
            bvParameters.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvreveal=debug";
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);

            BVSeoSdkUrl _bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParameters);
            BVFooter bvFooter = new BVHTMLFooter(bvConfiguration, bvParameters);
            bvFooter.setBvSeoSdkUrl(_bvSeoSdkUrl);

            String displayFooter = bvFooter.displayFooter("getContent");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"mt\">bvseo-LOCAL</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"ct\">bvseo-${_bvParameters.ContentType.uriValue()}</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"st\">bvseo-product</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"loadSEOFilesLocally\">true</li>"), true, "the content string should match.");
        }

        [TestMethod]
        public void TestDisplayFooter_URL_Debug()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");

            BVParameters bvParameters = new BVParameters();
            bvParameters.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvreveal=debug";
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);

            BVSeoSdkUrl _bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParameters);
            BVFooter bvFooter = new BVHTMLFooter(bvConfiguration, bvParameters);
            bvFooter.setBvSeoSdkUrl(_bvSeoSdkUrl);

            String displayFooter = bvFooter.displayFooter("getContent");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"mt\">bvseo-CLOUD</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"ct\">bvseo-reviews</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"st\">bvseo-product</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"contentURL\">http://seo.bazaarvoice.com"), true, "the content string should match.");

            /** When loading from files it should not display URL. **/
            bvConfiguration = new BVSdkConfiguration();
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            bvParameters = new BVParameters();
            bvParameters.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvreveal=debug";
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParameters.ContentType = new BVContentType(BVContentType.REVIEWS);

            _bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParameters);
            bvFooter = new BVHTMLFooter(bvConfiguration, bvParameters);
            bvFooter.setBvSeoSdkUrl(_bvSeoSdkUrl);

            displayFooter = bvFooter.displayFooter("getContent");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"mt\">bvseo-LOCAL</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"ct\">bvseo-reviews</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"st\">bvseo-product</li>"), true, "the content string should match.");
            Assert.AreEqual<Boolean>(displayFooter.Contains("<li id=\"contentURL\">http://seo.bazaarvoice.com"), false, "there should not be any url pattern.");
        }


    }
}
