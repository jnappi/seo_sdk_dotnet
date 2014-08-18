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

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVUIContentServiceProviderTest
    /// </summary>
    [TestClass]
    public class BVUIContentServiceProviderTest
    {
        private BVUIContentService bvUIContentService;
        private BVConfiguration bvConfiguration;

        public BVUIContentServiceProviderTest()
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


        /// <summary>
        /// Use BVUIContentServiceProviderTestInitialize to run code before running each test
        /// </summary>
        [TestInitialize()]
        public void BVUIContentServiceProviderTestInitialize() 
        {
            bvConfiguration = new BVSdkConfiguration();
        }

        /// <summary>
        /// Test case to check if showUserAgentSEOContent is working as expected.
        /// Refer to implementation on various cases how this is achieved.
        /// </summary>
        [TestMethod]
        public void TestShowUserAgentSeoContent()
        {
            bvUIContentService = new BVUIContentServiceProvider(bvConfiguration);

            BVParameters bvParameters = null;
            bvUIContentService.setBVParameters(bvParameters);

            Boolean showUserAgent = bvUIContentService.showUserAgentSEOContent();
            StringBuilder message = bvUIContentService.getMessage();
            Assert.IsFalse(showUserAgent, "value retuned should be false");
            Assert.IsTrue(String.IsNullOrEmpty(message.ToString()), "there should not be any message.");

            bvParameters = new BVParameters();
            bvUIContentService.setBVParameters(bvParameters);
            showUserAgent = bvUIContentService.showUserAgentSEOContent();
            message = bvUIContentService.getMessage();
            Assert.IsFalse(showUserAgent, "value retuned should be false");
            Assert.IsTrue(String.IsNullOrEmpty(message.ToString()), "there should not be any message.");

            bvParameters = new BVParameters();
            bvParameters.UserAgent = "some other string representation";
            bvUIContentService.setBVParameters(bvParameters);
            showUserAgent = bvUIContentService.showUserAgentSEOContent();
            message = bvUIContentService.getMessage();
            Assert.IsFalse(showUserAgent, "value retuned should be false");
            Assert.IsTrue(String.IsNullOrEmpty(message.ToString()), "there should not be any message.");

            /** Custom configuration for crawler agent **/
            bvConfiguration.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "custombot");
            bvParameters = new BVParameters();
            bvParameters.UserAgent = "custombot";
            bvUIContentService.setBVParameters(bvParameters);
            showUserAgent = bvUIContentService.showUserAgentSEOContent();
            message = bvUIContentService.getMessage();
            Assert.IsTrue(showUserAgent, "value retuned should be true");
            Assert.IsTrue(String.IsNullOrEmpty(message.ToString()), "there should not be any message.");

            /** Custom crawler but with google user agent. **/
		    bvConfiguration.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "custombot");
            bvParameters = new BVParameters();
            bvParameters.UserAgent = "googlebot";
		    bvUIContentService.setBVParameters(bvParameters);
		    showUserAgent = bvUIContentService.showUserAgentSEOContent();
		    message = bvUIContentService.getMessage();
            Assert.IsTrue(showUserAgent, "value retuned should be true");
            Assert.IsTrue(String.IsNullOrEmpty(message.ToString()), "there should not be any message.");
        }

        /// <summary>
        /// Test case to check if sdkEnabled gives the expected result.
        /// </summary>
        [TestMethod]
        public void TestSDKEnabled()
        {
            bvUIContentService = new BVUIContentServiceProvider(bvConfiguration);
            BVParameters bvParameters = null;

            bvUIContentService.setBVParameters(bvParameters);
            bool isSdkEnabled = bvUIContentService.isSdkEnabled();
            Assert.IsTrue(isSdkEnabled, "SDK enabled should be true here.");

            /** Disabled behavior check. **/
            bvConfiguration.addProperty(BVClientConfig.SEO_SDK_ENABLED, "False");
            bvParameters = new BVParameters();
            bvParameters.PageURI = "http://localhost:8080/sampleapp/thecontent.jsp?product=abc";
            BVSeoSdkUrl bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParameters);
            bvUIContentService = new BVUIContentServiceProvider(bvConfiguration);
            bvUIContentService.setBVSeoSdkUrl(bvSeoSdkUrl);
            isSdkEnabled = bvUIContentService.isSdkEnabled();
            Assert.IsFalse(isSdkEnabled, "SDK enabled should be false here.");

            /** Disable SDK but upon bvreveal sdkEnabled should be true. **/
            bvConfiguration.addProperty(BVClientConfig.SEO_SDK_ENABLED, "False");
            bvParameters = new BVParameters();
            bvParameters.PageURI = "http://localhost:8080/sampleapp/thecontent.jsp?product=abc&bvreveal=debug";
            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParameters);
            bvUIContentService = new BVUIContentServiceProvider(bvConfiguration);
            bvUIContentService.setBVSeoSdkUrl(bvSeoSdkUrl);
            isSdkEnabled = bvUIContentService.isSdkEnabled();
            Assert.IsTrue(isSdkEnabled, "SDK enabled should be true here.");
        }
    }
}
