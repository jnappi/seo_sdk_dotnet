using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BVSeoSdkDotNet.Model;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVParametersTest
    /// </summary>
    [TestClass]
    public class BVParametersTest
    {
        public BVParametersTest()
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
        public void TestEquality()
        {
            BVParameters bvParamObj1 = new BVParameters();
            bvParamObj1.UserAgent = "googlebot";
            bvParamObj1.BaseURI = "Example-Vector.jsp"; 
            bvParamObj1.PageURI = "http://localhost/Example-Vector.jsp?someQuery=value1";
            bvParamObj1.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParamObj1.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParamObj1.SubjectId = "1501";

            BVParameters bvParamObj2 = null;

            Assert.AreEqual<Boolean>(bvParamObj1.Equals(bvParamObj2), false , "object1 and object2 should not be equal");

            bvParamObj2 = bvParamObj1;
            Assert.AreEqual<Boolean>(bvParamObj1.Equals(bvParamObj2), true, "object1 and object2 should be equal");

            /*
             * Other object instance test
             */
            Assert.AreEqual<Boolean>(bvParamObj1.Equals("ABCD"), false, "object1 and someother should not be equal");

            bvParamObj2 = new BVParameters();
            bvParamObj2.UserAgent = "msnbot";
            bvParamObj2.BaseURI = "Example-Vector.jsp";
            bvParamObj2.PageURI = "http://localhost/Example-Vector.jsp?someQuery=value1";
            bvParamObj2.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParamObj2.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParamObj2.SubjectId = "1501";
            Assert.AreEqual<Boolean>(bvParamObj1.Equals(bvParamObj2), false, "object1 and object2 should not be equal");

        }
    }
}
