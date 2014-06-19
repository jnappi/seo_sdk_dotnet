using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;
using BVSeoSdkDotNet.BVException;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVSdkMessageUtilTest
    /// </summary>
    [TestClass]
    public class BVSdkMessageUtilTest
    {
        public BVSdkMessageUtilTest()
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
        /// Tests to check if the message classes are able to get the messages
        /// </summary>
        [TestMethod]
        public void TestMessages()
        {
            /** Null message code **/
            String messageCode = null;
            String message = null;
            String errorMessage = null;
            try
            {
                message = BVMessageUtil.getMessage(messageCode);
            }
            catch (BVSdkException bvExc)
            {
                errorMessage = bvExc.getMessage();
            }
            Assert.IsNull(message, "message should be null here");
            Assert.IsNotNull(errorMessage, "There should be an error message in errorMessage");

            /*
             * Empty message code
             */
            messageCode = "";
            errorMessage = null;
            try
            {
                message = BVMessageUtil.getMessage(messageCode);
            }
            catch (BVSdkException bvExc)
            {
                errorMessage = bvExc.getMessage();
            }
            Assert.IsNull(message, "message should be null here");
            Assert.IsNotNull(errorMessage, "There should be an error message in errorMessage");

            /*
             * Invalid message code
             */
            messageCode = "INVALID_CODE";
            errorMessage = null;
            try
            {
                message = BVMessageUtil.getMessage(messageCode);
            }
            catch (BVSdkException bvExc)
            {
                errorMessage = bvExc.getMessage();
                Assert.Fail("There was an error please check the exception which should have not occured.");
            }
            Assert.AreEqual<string>(message, messageCode, "message should be same as messageCode");
            Assert.IsNull(errorMessage, "There should not be an error message in errorMessage");

            /*
             * Valid message code
             */
            messageCode = "MSG0000";
            errorMessage = null;
            try
            {
                message = BVMessageUtil.getMessage(messageCode);
            }
            catch (BVSdkException bvExc)
            {
                errorMessage = bvExc.getMessage();
            }
            Assert.IsNotNull(message, "message should not be null.");
            Assert.IsNull(errorMessage, "There should not be an error message in errorMessage");
        }
    }
}
