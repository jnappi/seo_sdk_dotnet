using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BVSEOSDKTest
{
    [TestClass]
    public class BvProxyTest
    {
        private const string CloudKey = "myshco-126b543c32d9079f120a575ece25bad6";
        private const string BvRootFolder = "9344ia";
        private const string Id = "5000001";

        private static BVConfiguration GetCommonConfig()
        {
            var config = new BVSdkConfiguration();
            config.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            config.addProperty(BVClientConfig.CLOUD_KEY, CloudKey);
            config.addProperty(BVClientConfig.BV_ROOT_FOLDER, BvRootFolder);
            config.addProperty(BVClientConfig.STAGING, "true");
            return config;
        }

        private static BVParameters GetCommonParams()
        {
            return new BVParameters
            {
                UserAgent = "google",
                PageURI = "http://localhost/MyApp/?bvpage=ctre/id5000001/stp"
            };
        }

        [TestMethod]
        public void TestWithoutProxy()
        {
            var bvConfig = GetCommonConfig();
            var uiContent = new BVManagedUIContent(bvConfig);
            var bvParameters = GetCommonParams();
            var theUiContent = uiContent.getContent(bvParameters);

            Assert.AreEqual(theUiContent.Contains("BVRRSourceID"), true, "there should be BvRRSourceID in the content");
            Assert.AreEqual(theUiContent.Contains("bvseo-msg: Connect to localhost:9999 timed out"), false,
                "there should not be connection a timed out message.");
        }

        [TestMethod]
        public void TestProxyImplementation_Failure()
        {
            var bvConfig = GetCommonConfig();
            bvConfig.addProperty(BVClientConfig.PROXY_HOST, "localhost");
            bvConfig.addProperty(BVClientConfig.PROXY_PORT, "9999");

            var uiContent = new BVManagedUIContent(bvConfig);
            var bvParameters = GetCommonParams();
            var theUiContent = uiContent.getContent(bvParameters);

            Assert.AreEqual(theUiContent.Contains("BVRRSourceID"), false, "there should not be BvRRSourceID in the content");
            Assert.AreEqual(theUiContent.Contains(" The resource to the URL or file is currently unavailable"), true);
        }

        [TestMethod]
        public void TestProxyImplementation_Success()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://+:12345/");
            listener.Start();
            IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback), listener);

            var bvConfig = GetCommonConfig();
            bvConfig.addProperty(BVClientConfig.PROXY_HOST, "localhost");
            bvConfig.addProperty(BVClientConfig.PROXY_PORT, "12345");

            var uiContent = new BVManagedUIContent(bvConfig);
            var bvParameters = GetCommonParams();
            var theUiContent = uiContent.getContent(bvParameters);

            result.AsyncWaitHandle.WaitOne();
            Assert.AreEqual(theUiContent.Contains("BVRRSourceID"), true, "there should be BvRRSourceID in the content");
            Assert.AreEqual(theUiContent.Contains("bvseo-msg: Connect to localhost:9999 timed out"), false,
                "there should not be connection a timed out message.");
        }

        public void ListenerCallback(IAsyncResult result)
        {
            var listener = (HttpListener)result.AsyncState;
            var context = listener.EndGetContext(result);
            var request = context.Request;

            Debug.WriteLine("Contents are available @ : " + request.RawUrl);
            Assert.AreEqual(request.RawUrl.Contains(CloudKey), true);
            Assert.AreEqual(request.RawUrl.Contains(BvRootFolder), true);

            var response = context.Response;
            var output = response.OutputStream;

            var webClient = new WebClient();
            var responseFromS3 = webClient.DownloadString(request.RawUrl);

            Debug.WriteLine("Received contents from S3: " + responseFromS3);

            var buffer = Encoding.UTF8.GetBytes(responseFromS3);
            response.ContentLength64 = buffer.Length;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

            listener.Close();
        }
    }
}
