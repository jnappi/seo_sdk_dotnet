using System;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class ExampleGetSpotlights : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            String cloudKey = Request.QueryString["cloudkey"];
            String staging = Request.QueryString["staging"];
            String testing = Request.QueryString["testing"];
            String rootFolder = Request.QueryString["site"];
            String category = Request.QueryString["category"];
            String noBodySchema = Request.QueryString["noBodySchema"];
            String subjectId = "category-1";

            if (cloudKey != null)
            {
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, cloudKey);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "spotlight-four-746e2fc1211dc8964560350c9f28b67a");
            }
            if (staging != null)
            {
                bvConfig.addProperty(BVClientConfig.STAGING, staging);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.STAGING, "false");
            }
            if (testing != null)
            {
                bvConfig.addProperty(BVClientConfig.TESTING, testing);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.TESTING, "true");
            }

            if (rootFolder != null)
            {
                bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, rootFolder);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            }

            if (category != null)
            {
                subjectId = category;
            }
            else
            {
                subjectId = "category-1";
            }

            if (noBodySchema == null)
            {
                pageBody.Attributes.Add("itemscope itemtype", "http://schema.org/WebPage");
            }

            var bvParameters = new BVParameters
            {
                BaseURI =
                    Request.Url.ToString().Contains("?")
                        ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?"))
                        : Request.Url.ToString(),
                PageURI = Request.Url.ToString(),
                ContentType = new BVContentType(BVContentType.SPOTLIGHTS),
                SubjectType = new BVSubjectType(BVSubjectType.CATEGORY),
                SubjectId = subjectId
            };
            
            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            BVSpotlightsContainer.InnerHtml = bvOutput.getContent(bvParameters);
            BVSEOURL.InnerHtml = bvOutput.getUrl();
            
        }
    }
}