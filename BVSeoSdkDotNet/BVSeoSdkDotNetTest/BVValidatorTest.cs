using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.Validation;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVValidatorTest
    /// </summary>
    [TestClass]
    public class BVValidatorTest
    {
        private BVConfiguration bvConfig;
        private BVParameters bvParams;
        private BVValidator bvValidator;
        private String errorMessage;

        public BVValidatorTest()
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
        /// test case for validate method in BVParameterValidator.
        /// This test case starts from failure scenario to till success scenario.
        /// </summary>
        [TestMethod]
        public void TestValidation_When_BVConfig_Is_Null()
        {
            bvConfig = null;
            bvParams = null;
            bvValidator = new BVDefaultValidator();
            errorMessage = bvValidator.validate(bvConfig, bvParams);
            Assert.AreEqual<Boolean>(errorMessage.Contains("BVConfiguration is null, please set a valid BVConfiguration.;"), true, "Error Messages are different.");

            bvConfig = new BVSdkConfiguration();
            bvParams = new BVParameters();
            bvValidator = new BVDefaultValidator();
            errorMessage = bvValidator.validate(bvConfig, bvParams);

            Assert.AreEqual<Boolean>(errorMessage.Contains("SubjectId cannot be null or empty.;"), true, "Error Messages are different.");
        }
    }
}
