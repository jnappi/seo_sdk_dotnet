using System;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class ExampleMyscho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");  // set to true for staging environment data
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "1500");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT, "2000");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-3e3001e88d9c32d19a17cafacb81bec7");
            bvConfig.addProperty(BVClientConfig.STAGING, "true");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344");

            var bvParameters = new BVParameters
            {
                BaseURI =
                    Request.Url.ToString().Contains("?")
                        ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?"))
                        : Request.Url.ToString(),
                PageURI = Request.Url.ToString(),
                UserAgent = Request.UserAgent,
                ContentType = new BVContentType(BVContentType.REVIEWS),
                SubjectType = new BVSubjectType(BVSubjectType.PRODUCT),
                SubjectId = "5000001"
            };

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            BVRRSummaryContainer.InnerHtml = bvOutput.getAggregateRating(bvParameters);
            BVRRContainer.InnerHtml = bvOutput.getReviews(bvParameters);
        }
    }
}