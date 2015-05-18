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
            bvConfiguration.addProperty(BVClientConfig.CLOUD_KEY,"myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca");

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
            String expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/1/ssl-certificates.htm";

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();


            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Console.WriteLine("1 "+actualSeoContentUri + "\n" + expectedSeoContentUri + "\n\n");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri are empty. **/
            bvParam.BaseURI = "";
            bvParam.PageURI = "";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Console.WriteLine("2 "+ actualSeoContentUri + "\n" + expectedSeoContentUri + "\n\n");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri are complete urls. **/
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvrrp=abcd";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvrrp=abcd";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Console.WriteLine("3 "+actualSeoContentUri + "\n" + expectedSeoContentUri + "\n\n");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?null";
            expectedQueryString = "?null&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/3/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Console.WriteLine("4 " + actualSeoContentUri + "\n" + expectedSeoContentUri + "\n\n");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Console.WriteLine(actualBaseUri);
            Console.WriteLine(actualQueryString);
            Console.WriteLine(actualSeoContentUri);

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Console.WriteLine("5 "+ actualSeoContentUri + "\n" + expectedSeoContentUri + "\n\n");
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
            bvParam.PageNumber = "";

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
            bvParam.PageNumber = "";

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
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedSeoContentUri = "/filePath/6574-en_us/reviews/product/3/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, new Uri(Path.GetFullPath(expectedSeoContentUri)).AbsoluteUri.ToString(), "actual and expected seo content uri should be same");

            /** When base uri and page uri has bvrrp parameters. **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvrrp=6574-en_us/reviews/product/2/ssl-certificates.htm";
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
            String expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviewsPage/product/2/ssl-certificates.htm";

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
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=pg2/ctrp/stp/iddogfood";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=pg3/ctre/stp/idcatfood";
            bvParam.PageNumber = "";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvpage=pg3/ctre/stp/idcatfood";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/3/catfood.htm";

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
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=pg3/ctre/stp/idcatfood";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=pg4/ctrp/stp";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "p5543";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvpage=pg4/ctrp/stp";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviewsPage/product/4/p5543.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** reviews category roll-up (future product), page 2, subject ID = c8765 **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=pg2/ctre/stc";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "c8765";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?a=b&bvpage=pg2/ctre/stc";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/category/2/c8765.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** questions/answers detail page, question ID = q45677 **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=ctqa/std/id45677";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "c8765";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?a=b&bvpage=ctqa/std/id45677";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/questions/detail/1/45677.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** entry/landing page (future product), id = myshirtspage **/
            bvParam.BaseURI = null;
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvpage=ctun/ste/idmyshirtspage";
            bvParam.PageNumber = "";
            bvParam.SubjectId = null;

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "";
            expectedQueryString = "?a=b&bvpage=ctun/ste/idmyshirtspage";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/universal/entry/1/myshirtspage.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestUri_For_bvstate_HTTP()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates";
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "?bvstate=pg:2/ct:r/st:p/id:ssl-certificates";
            String expectedFragmentString = "";
            String expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/ssl-certificates.htm";

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualFragmentString = bvSeoSdkUrl.fragmentString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate query parameters. bvstate can have reveal:debug
             * baseURI has bvstate query parameter - corretedBaseUri needs to have this removed
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/reveal:debug/id:ssl-certificates";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/reveal:debug/id:ssl-certificates";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvstate=pg:2/ct:r/st:p/reveal:debug/id:ssl-certificates";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate query parameters
             * bvstate can have reveal:notdebug
             * bvstate has no subjecttype - should check whether it is defaulted to product
             * bvstate has different subjectid - contentUri should reflect picking this subjectId
             * base uri has different page number and subjectid - contentUri should reflect page uri data
             * Testcase 1: SubjectType Defaults to appropriate value
             * Testcase 2: bvstate is pickedup from pageURI
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:3/ct:r/reveal:notdebug/id:web_hosting";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/reveal:notdebug/id:domains";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvstate=pg:2/ct:r/reveal:notdebug/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate query parameters with no contenttype
             * Testcase: Ignore bvstate if contentType is missing
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/st:p/id:domains";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            expectedQueryString = "?bvstate=pg:2/st:p/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/1/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate query parameters with no page
             * Testcase: Default PageNumber to 1 when not present in bvstate.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?a=b&bvstate=ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?a=b&bvstate=ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b";
            expectedQueryString = "?a=b&bvstate=ct:r/st:p/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/1/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate query parameters with different contentType than in bvParam
             * Testcase: Ignore bvstate values, assumption is that it belongs to a different bv content on the same page 
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?a=b&bvstate=pg:3/ct:q/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?a=b&bvstate=pg:3/ct:q/st:p/id:domains";
            bvParam.PageNumber = "2";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b";
            expectedQueryString = "?a=b&bvstate=pg:3/ct:q/st:p/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate hashbang
             * Testcase: Set contentUri based on bvstate hashbang.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp#!bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp#!bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp#!";
            expectedQueryString = "";
            expectedFragmentString = "#!bvstate=pg:2/ct:r/st:p/id:domains";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate hashbang. bvstate present after ? on the hashbang
             * Testcase: Set contentUri based on bvstate hashbang.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp#!a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp#!a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp#!a/b";
            expectedQueryString = "";
            expectedFragmentString = "#!a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate hashbang. bvstate present after ? and & on the hashbang
             * Testcase: Set contentUri based on bvstate hashbang.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp#!a/b?c=d&bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp#!a/b?c=d&bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp#!a/b?c=d";
            expectedQueryString = "";
            expectedFragmentString = "#!a/b?c=d&bvstate=pg:2/ct:r/st:p/id:domains";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate _escaped_fragment_
             * Testcase: Set contentUri based on bvstate _escaped_fragment_.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains/reveal:debug";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains/reveal:debug";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=";
            expectedQueryString = "?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains/reveal:debug";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate _escaped_fragment_ + _escaped_fragment_ has more data with %26 separator
             * Testcase: Set contentUri based on bvstate _escaped_fragment_.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a=b";
            expectedQueryString = "?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate _escaped_fragment_ + _escaped_fragment_ has bvstate after ? separator
             * Testcase: Set contentUri based on bvstate _escaped_fragment_.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b";
            expectedQueryString = "?_escaped_fragment_=a/b?bvstate=pg:2/ct:r/st:p/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate _escaped_fragment_ + _escaped_fragment_ has bvstate after ? separator and %26
             * Testcase: Set contentUri based on bvstate _escaped_fragment_.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b?c=d%26bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b?c=d%26bvstate=pg:2/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a/b?c=d";
            expectedQueryString = "?_escaped_fragment_=a/b?c=d%26bvstate=pg:2/ct:r/st:p/id:domains";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate in _escaped_fragment_ and hashbang fragment.
             * Testcase: Set contentUri based on bvstate hashbang fragment.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b#!bvstate=pg:4/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b#!bvstate=pg:4/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a=b#!";
            expectedQueryString = "?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            expectedFragmentString = "#!bvstate=pg:4/ct:r/st:p/id:domains";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/4/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate in _escaped_fragment_ and query parameter.
             * Testcase: Set contentUri based on bvstate _escaped_fragment_.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=a=b";
            expectedQueryString = "?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:domains%26a=b";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate in query parameter and empty _escaped_fragment_.
             * Testcase: Set contentUri based on bvstate from query parameter.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?_escaped_fragment_=";
            expectedQueryString = "?bvstate=pg:4/ct:r/st:p/id:domains&_escaped_fragment_=";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/4/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate in query parameter and hashbang fragment.
             * Testcase: Set contentUri based on bvstate hashbang fragment.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:domains&a=b#!bvstate=pg:4/ct:r/st:p/id:domains";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:domains&a=b#!bvstate=pg:4/ct:r/st:p/id:domains";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b#!";
            expectedQueryString = "?bvstate=pg:2/ct:r/st:p/id:domains&a=b";
            expectedFragmentString = "#!bvstate=pg:4/ct:r/st:p/id:domains";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/4/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate and bvpage.
             * Testcase: Set contentUri based on bvstate.
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/ct:r/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates";
            bvParam.PageNumber = "3";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b";
            expectedQueryString = "?bvstate=pg:4/ct:r/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/4/domains.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate, bvpage and bvrrp. bvstate has no contentType
             * Testcase: Set contentUri based on bvpage. (no valid bvstate hence fallback to bvpage)
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b";
            expectedQueryString = "?bvstate=pg:4/st:p/id:domains&a=b&bvpage=pg2/ctre/stp/ssl-certificates&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/2/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /*
             * base uri and page uri has bvstate and bvrrp. bvstate has no contentType
             * Testcase: Set contentUri based on bvrrp. (no valid bvstate hence fallback to bvrrp)
             */

            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/st:p/id:domains&a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:4/st:p/id:domains&a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            bvParam.PageNumber = "";
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp?a=b";
            expectedQueryString = "?bvstate=pg:4/st:p/id:domains&a=b&bvrrp=6574-en_us/reviews/product/3/ssl-certificates.htm";
            expectedFragmentString = "";
            expectedSeoContentUri = "http://seo.bazaarvoice.com/myshco-359c29d8a8cbe3822bc0d7c58cb9f9ca/6574-en_us/reviews/product/3/ssl-certificates.htm";

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualFragmentString = bvSeoSdkUrl.fragmentString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualFragmentString, expectedFragmentString, "actual and expected fragment string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");
        }

        [TestMethod]
        public void TestUri_For_bvstate_File()
        {
            bvConfiguration.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");

            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p";
            bvParam.ContentType = new BVContentType(BVContentType.REVIEWS);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/Sample/Example-1.jsp";
            String expectedQueryString = "?bvstate=pg:2/ct:r/st:p";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/reviews/product/2/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

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
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/reviewsPage/product/2/ssl-certificates.htm")).AbsoluteUri.ToString();

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
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            bvParam.ContentType = new BVContentType(BVContentType.STORIES);
            bvParam.SubjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_GRID);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            String expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            String expectedQueryString = "?a=b&bvsyp=6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm";
            String expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/2/storiesgrid/ssl-certificates.htm")).AbsoluteUri.ToString();

            String actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            String actualQueryString = bvSeoSdkUrl.queryString();
            String actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** Test case - 2 **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            bvParam.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_LIST);
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvsyp=6574-en_us/stories/product/2/stories/ssl-certificates.htm";
            expectedSeoContentUri = new Uri(Path.GetFullPath("/filePath/6574-en_us/stories/product/2/stories/ssl-certificates.htm")).AbsoluteUri.ToString();

            actualBaseUri = bvSeoSdkUrl.correctedBaseUri();
            actualQueryString = bvSeoSdkUrl.queryString();
            actualSeoContentUri = bvSeoSdkUrl.seoContentUri().ToString();

            Assert.AreEqual<string>(actualBaseUri, expectedBaseUri, "actual and expected base uri should be same");
            Assert.AreEqual<string>(actualQueryString, expectedQueryString, "actual and expected query string should be same");
            Assert.AreEqual<string>(actualSeoContentUri, expectedSeoContentUri, "actual and expected seo content uri should be same");

            /** Test case - 3 **/
            bvParam.BaseURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            bvParam.PageURI = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
            bvParam.ContentSubType = null;
            bvParam.SubjectId = "ssl-certificates";

            bvSeoSdkUrl = new BVSeoSdkURLBuilder(bvConfiguration, bvParam);

            expectedBaseUri = "http://localhost:8080/sample_seo_sdk_web/scenario-2.jsp?a=b";
            expectedQueryString = "?a=b&bvsyp=6574-en_us/stories/product/2/ssl-certificates.htm";
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
