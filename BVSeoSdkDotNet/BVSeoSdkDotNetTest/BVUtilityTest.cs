using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using BVSeoSdkDotNet.Model;
using BVSeoSdkDotNet.Util;

namespace BVSEOSDKTest
{
    /// <summary>
    /// Summary description for BVUtilityTest
    /// </summary>
    [TestClass]
    public class BVUtilityTest
    {

        public BVUtilityTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        /// <summary>
        /// Test case to check if sdkEnabled gives the expected result.
        /// </summary>
        [TestMethod]
        public void TestPageURIReplacementInContent()
        {
            /*
             * bvrrp in content with no query/fragment string in baseUri.
             * append ?
             */
            String data = "{INSERT_PAGE_URI}bvrrp=9344/reviews/product/4/5000001.htm";
            StringBuilder content = new StringBuilder();
            content.Append(data);
            String baseUri = "http://localhost:8080/index.jsp";
            String expectedContent = "http://localhost:8080/index.jsp?bvrrp=9344/reviews/product/4/5000001.htm";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing query string in baseUri.
             * append &
             */
            data = "{INSERT_PAGE_URI}bvrrp=9344/reviews/product/4/5000001.htm";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp?a=b";
            expectedContent = "http://localhost:8080/index.jsp?a=b&bvrrp=9344/reviews/product/4/5000001.htm";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with no query/fragment string in baseUri.
             * append ?
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp";
            expectedContent = "http://localhost:8080/index.jsp?bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing query string in baseUri.
             * append &
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp?a=b";
            expectedContent = "http://localhost:8080/index.jsp?a=b&bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing fragment string in baseUri.
             * baseUri - ends with #!
             * append nothing
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp#!";
            expectedContent = "http://localhost:8080/index.jsp#!bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing fragment string in baseUri.
             * baseUri - not end with #! but ends with ?
             * append nothing
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp#!a=b?";
            expectedContent = "http://localhost:8080/index.jsp#!a=b?bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing fragment string in baseUri.
             * baseUri - not end with #! and not ends with ? but contains ?
             * append &
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp#!a=b?testing=true";
            expectedContent = "http://localhost:8080/index.jsp#!a=b?testing=true&bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing fragment string in baseUri.
             * baseUri - not end with #! and doesnot contain ?
             * append ?
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp#!a=b";
            expectedContent = "http://localhost:8080/index.jsp#!a=b?bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing _escaped_fragment_ string in baseUri.
             * baseUri - ends with _escaped_fragment_=
             * append nothing
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp?_escaped_fragment_=";
            expectedContent = "http://localhost:8080/index.jsp?_escaped_fragment_=bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing _escaped_fragment_ string in baseUri.
             * baseUri - contains _escaped_fragment_ with ? in it
             * append %26
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp?_escaped_fragment_=a/b?c=d";
            expectedContent = "http://localhost:8080/index.jsp?_escaped_fragment_=a/b?c=d%26bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");

            /*
             * bvstate in content with existing _escaped_fragment_ string in baseUri.
             * baseUri - contains _escaped_fragment_ with no ? in it
             * append ?
             */
            data = "{INSERT_PAGE_URI}bvstate=pg:2/ct:r/st:p/id:5000001";
            content = new StringBuilder();
            content.Append(data);
            baseUri = "http://localhost:8080/index.jsp?_escaped_fragment_=a/b";
            expectedContent = "http://localhost:8080/index.jsp?_escaped_fragment_=a/b?bvstate=pg:2/ct:r/st:p/id:5000001";
            BVUtility.replacePageURIFromContent(content, baseUri);

            Assert.AreEqual<string>(content.ToString(), expectedContent, "actual and expected content should be same");
        }

        [TestMethod]
        public void TestIsRevealDebugEnabled()
        {
            // No bvreveal=debug in query param or reveal:debug in bvstate
            BVParameters bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates";

            Assert.IsFalse(BVUtility.isRevealDebugEnabled(bvParam), "reveal debug should be false");

            // Has bvreveal=notdebug in query param. No reveal:debug in bvstate
            bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates&bvreveal=notdebug";

            Assert.IsFalse(BVUtility.isRevealDebugEnabled(bvParam), "reveal debug should be false");

            // Has bvreveal=debug in query param 
            bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates&bvreveal=debug";

            Assert.IsTrue(BVUtility.isRevealDebugEnabled(bvParam), "reveal debug should be true");

            // Has reveal:notdebug in bvstate 
            bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates/reveal:notdebug";

            Assert.IsFalse(BVUtility.isRevealDebugEnabled(bvParam), "reveal debug should be false");

            // Has reveal:debug in bvstate 
            bvParam = new BVParameters();
            bvParam.BaseURI = "http://localhost:8080/Sample/Example-1.jsp";
            bvParam.PageURI = "http://localhost:8080/Sample/Example-1.jsp?bvstate=pg:2/ct:r/st:p/id:ssl-certificates/reveal:debug";

            Assert.IsTrue(BVUtility.isRevealDebugEnabled(bvParam), "reveal debug should be true");
        }
    }
}
