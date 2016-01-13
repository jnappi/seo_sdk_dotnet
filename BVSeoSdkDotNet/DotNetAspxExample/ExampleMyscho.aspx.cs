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

            String cloudKey = Request.QueryString["cloudkey"];
            String staging = Request.QueryString["staging"];
            String testing = Request.QueryString["testing"];
            String rootFolder = Request.QueryString["site"];
            String productIdParam = Request.QueryString["productid"];
            
            String subjectId = "5000001";

            if (cloudKey != null)
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, cloudKey);
            else
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-3e3001e88d9c32d19a17cafacb81bec7");
            if (staging != null)
                bvConfig.addProperty(BVClientConfig.STAGING, staging);
            else
                bvConfig.addProperty(BVClientConfig.STAGING, "true");
            if (testing != null)
                bvConfig.addProperty(BVClientConfig.TESTING, testing);
            else
                bvConfig.addProperty(BVClientConfig.TESTING, "false");

            if (rootFolder != null)
                bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, rootFolder);
            else
                bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344");

            if (productIdParam != null)
            {
                //if productIdParameter is null then use the default value.  If it's not null then use the parameter.  If category
                subjectId = productIdParam;
            }
            else
                subjectId = "5000001";

            bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");
            
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "1500");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT, "2000");
            
            var bvParameters = new BVParameters
            {
                BaseURI =
                    Request.Url.ToString().Contains("?")
                        ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?"))
                        : Request.Url.ToString(),
                PageURI = Request.Url.ToString(),
                ContentType = new BVContentType(BVContentType.REVIEWS),
                SubjectType = new BVSubjectType(BVSubjectType.PRODUCT),
                SubjectId = subjectId
            };

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            BVRRSummaryContainer.InnerHtml = bvOutput.getAggregateRating(bvParameters);
            BVRRContainer.InnerHtml = bvOutput.getReviews(bvParameters);
        }
    }
}