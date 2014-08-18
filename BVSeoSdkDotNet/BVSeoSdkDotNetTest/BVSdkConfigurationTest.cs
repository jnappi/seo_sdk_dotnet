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
    /// Test class for BVSdkConfiguration implementation class.
    /// Check individual docs on each test methods provided in this test class
    /// for detailed information.
    /// 
    /// @author Mohan Krupanandan
    /// </summary>
    [TestClass]
    public class BVSdkConfigurationTest
    {
        public BVSdkConfigurationTest()
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
        /// Test case to check if bvconfig.properties loads properly.
        /// Assertions are made randomly to cross verify on the configurations that are provided by default.
        /// Any change in the property may lead to failure in the test case
        /// which should be corrected.
        /// 
        /// There are no possible error scenarios arising since the property file loads from the 
        /// resource bundle that is already provided.
        /// </summary>
        [TestMethod]
        public void TestBVConfigLoading()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            String stagingS3Hostname = bvConfiguration.getProperty("stagingS3Hostname");
            Assert.AreEqual<string>(stagingS3Hostname, "seo-stg.bazaarvoice.com", "stagingS3Hostname are different.");

            String productionS3Hostname = bvConfiguration.getProperty("productionS3Hostname");
            Assert.AreEqual<string>(productionS3Hostname, "seo.bazaarvoice.com", "productionS3Hostname are different.");
        }

        /// <summary>
        /// Test case for both add and getProperty methods.
        /// Contains both positive scenario and negative scenario.
        /// Add and getProperty are interchangeably used hence
        /// this test case will server the purpose for both add and getProperty methods.
        /// </summary>
        [TestMethod]
        public void TestAdd_GetProperty()
        {
            BVConfiguration bvConfiguration = new BVSdkConfiguration();
            String actualErrorMessage = null;
            String depZoneId = null;
            String actualDepZoneId = null;

            /** Negative scenario. trying to add null value to existing value. **/
            try
            {
                bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, depZoneId);
            }
            catch (BVSdkException e)
            {
                actualErrorMessage = e.getMessage();
            }
            Assert.AreEqual<string>(actualErrorMessage, BVMessageUtil.getMessage("ERR0006"), "Error message does not match.");

            /** Negative scenario. trying to add empty value. **/
            actualErrorMessage = null;
            actualDepZoneId = null;
            depZoneId = "";
            try
            {
                bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, depZoneId);
            }
            catch (BVSdkException e)
            {
                actualErrorMessage = e.getMessage();
            }
            Assert.AreEqual<string>(actualErrorMessage, BVMessageUtil.getMessage("ERR0006"), "Error message does not match.");

            /** positive scenario. when adding valid value. **/
            actualErrorMessage = null;
            actualDepZoneId = null;
            depZoneId = "DEP_ZONE_ID";
            try
            {
                bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, depZoneId);
            }
            catch (BVSdkException e)
            {
                actualErrorMessage = e.getMessage();
                Assert.Fail("There was an exception please fix the code so that the exception will not occur.");
            }
            Assert.IsNull(actualErrorMessage, "There should not be any error message.");
            actualDepZoneId = bvConfiguration.getProperty(BVClientConfig.BV_ROOT_FOLDER);
            Assert.AreEqual<string>(depZoneId, actualDepZoneId, "deployment zone id does not match");

            /** positive scenario. value overwrite. **/
            actualErrorMessage = null;
            actualDepZoneId = null;
            depZoneId = "DEP_ZONE_ID_OVERWRITE";
            try
            {
                bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, depZoneId);
            }
            catch (BVSdkException e)
            {
                actualErrorMessage = e.getMessage();
                Assert.Fail("There was an exception please fix the code so that the exception will not occur.");
            }
            Assert.IsNull(actualErrorMessage, "There should not be any error message.");
            actualDepZoneId = bvConfiguration.getProperty(BVClientConfig.BV_ROOT_FOLDER);
            Assert.AreEqual<string>(depZoneId, actualDepZoneId, "deployment zone id does not match");

            /** positive scenario : when trying to get non-existence property. **/
            String nonExistingProperty = bvConfiguration.getProperty("NON_EXISTING_PROPERTY");
            Assert.IsNull(nonExistingProperty, "the nonExistingProperty should be null.");
        }

        /// <summary>
        /// Test case to test multiple instance support for BVSDKConfiguration.
        /// </summary>
        [TestMethod]
        public void TestMultipleConfigurations()
        {
            BVConfiguration configurationInstance_1 = new BVSdkConfiguration();
            configurationInstance_1.addProperty(BVClientConfig.BV_ROOT_FOLDER, "DEP_INST_1");

            BVConfiguration configurationInstance_2 = new BVSdkConfiguration();
            configurationInstance_2.addProperty(BVClientConfig.BV_ROOT_FOLDER, "DEP_INST_2");

            String deploymentInstance_1 = configurationInstance_1.getProperty(BVClientConfig.BV_ROOT_FOLDER);
            String deploymentInstance_2 = configurationInstance_2.getProperty(BVClientConfig.BV_ROOT_FOLDER);
            Assert.AreEqual<Boolean>(deploymentInstance_1.Equals(deploymentInstance_2,StringComparison.InvariantCultureIgnoreCase), false, "Instance configuration should not be same");
        }

        /// <summary>
        /// Test case to test if selected BVCore contents from bvconfig.properties are not changed
        /// </summary>
        [TestMethod]
        public void TestBVCoreConfig()
        {
            BVConfiguration configuration = new BVSdkConfiguration();
            String hostName = configuration.getProperty(BVCoreConfig.PRODUCTION_S3_HOSTNAME);
            Assert.AreEqual<string>(hostName, "seo.bazaarvoice.com", "production hostname has been changed please fix.");

            hostName = configuration.getProperty(BVCoreConfig.STAGING_S3_HOSTNAME);
            Assert.AreEqual<string>(hostName, "seo-stg.bazaarvoice.com", "staging hostname has been changed please fix.");
        }
    }
}
