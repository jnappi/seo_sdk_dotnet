using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
    /// Summary description for BVSeoSdkURLBuilderTest
    /// </summary>
    [TestClass]
    public class BVSeoSdkURLBuilderTest
    {
        private BVSeoSdkUrl bvSeoSdkUrl;
        private BVConfiguration bvConfiguration;

        public BVSeoSdkURLBuilderTest()
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
        public void BVSeoSdkURLBuilderTestInitialize()
        {
            bvConfiguration = new BVSdkConfiguration();

            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            bvConfiguration.addProperty(BVClientConfig.CLOUD_KEY,"godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4");

            bvConfiguration.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/filePath");
            bvConfiguration.addProperty(BVClientConfig.BV_ROOT_FOLDER, "6574-en_us");
        }

        [TestMethod]
        public void TestBase_PageUri_For_PRR_HTTP()
        {
            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = null;
            bvParam.PageURI = null;
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "";
            String expectedQueryString = "";
            String expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/1/ssl-certificates.htm";

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri are empty. **/
            bvParam.BaseURI = "";
            bvParam.PageURI = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri are complete urls. **/
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvrrp=abcd";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvrrp=abcd";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedQueryString = "?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/3/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            expectedQueryString = "?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestBase_PageUri_For_PRR_File()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = null;
            bvParam.PageURI = null;
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "";
            String expectedQueryString = "";
            String expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/1/ssl-certificates.htm";

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");

            /** When base uri and page uri are empty. **/
            bvParam.BaseURI = "";
            bvParam.PageURI = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "";
            expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");

            /** When base uri and page uri are complete urls. **/
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvrrp=abcd";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvrrp=abcd";
            expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");
            
            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedQueryString = "?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/3/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            expectedQueryString = "?null&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestUri_For_C2013_HTTP()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvpage=pg2/ctrp/stp";
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "?bvpage=pg2/ctrp/stp";
            String expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviewspage/product/2/ssl-certificates.htm";

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * When base uri and page uri has bvpage parameters.
             * and the subjectid is part of bvpage. also base uri has different page number and subjectid.
             * also the ct is reviews.
             */
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg2/ctrp/stp/iddogfood";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg3/ctre/stp/idcatfood";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg2/ctrp/stp/iddogfood";
            expectedQueryString = "?null&bvpage=pg3/ctre/stp/idcatfood";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/product/3/catfood.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
		     * When base uri and page uri has bvpage parameters but not a fully qualified url.
		     * also the subjectId is part of bvParam and not in the bvpage.
		     */
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg3/ctre/stp/idcatfood";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg4/ctrp/stp";
            bvParam.SubjectId = "p5543";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg3/ctre/stp/idcatfood";
            expectedQueryString = "?null&bvpage=pg4/ctrp/stp";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviewspage/product/4/p5543.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** reviews category roll-up (future product), page 2, subject ID = c8765 **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=pg2/ctre/stc";
            bvParam.SubjectId = "c8765";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?null&bvpage=pg2/ctre/stc";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/reviews/category/2/c8765.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** questions/answers detail page, question ID = q45677 **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=ctqa/std/id45677";
            bvParam.SubjectId = "c8765";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?null&bvpage=ctqa/std/id45677";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/questions/detail/1/45677.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** entry/landing page (future product), id = myshirtspage **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvpage=ctun/ste/idmyshirtspage";
            bvParam.SubjectId = null;

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?null&bvpage=ctun/ste/idmyshirtspage";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/godaddy-a4501eb5be8bf8efda68f3f4ff7b3cf4/6574-en_us/universal/entry/1/myshirtspage.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestUri_For_C2013_File()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvpage=pg2/ctrp/stp";
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "?bvpage=pg2/ctrp/stp";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/reviewspage/product/2/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestStory_Default()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/1/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }


        [TestMethod]
        public void TestStory_Default_None()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.NONE);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/1/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }


        [TestMethod]
        public void TestStory_Stories()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_LIST);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/1/stories/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }


        [TestMethod]
        public void TestStory_StoriesGrid()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_GRID);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/1/storiesgrid/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }


        [TestMethod]
        public void TestStory_Stories_PageNumber()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            /** Test case - 1 **/
            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_GRID);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            String expectedQueryString = "?null&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** Test case - 2 **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_LIST);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            expectedQueryString = "?null&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/2/stories/ssl-certificates.htm")).AbsoluteUri.ToString();

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** Test case - 3 **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            bvParam.ContentSubType = null;
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            expectedQueryString = "?null&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/2/ssl-certificates.htm")).AbsoluteUri.ToString();

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

        }
    }
}
