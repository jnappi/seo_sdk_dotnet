using System;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class ExampleGetSellerRatings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            String cloudKey = Request.QueryString["cloudkey"];
            String staging = Request.QueryString["staging"];
            String testing = Request.QueryString["testing"];
            String rootFolder = Request.QueryString["site"];
            String subjectId = "seller";

            if (cloudKey != null)
            {
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, cloudKey);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "srd-testcustomer-1-c3a130de760b3105e75e8202cb22e541");
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

            var bvParameters = new BVParameters
            {
                BaseURI =
                    Request.Url.ToString().Contains("?")
                        ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?"))
                        : Request.Url.ToString(),
                PageURI = Request.Url.ToString(),
                ContentType = new BVContentType(BVContentType.REVIEWS),
                SubjectType = new BVSubjectType(BVSubjectType.SELLER),
                SubjectId = subjectId
            };

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            BVSellerRatingsContainer.InnerHtml = bvOutput.getContent(bvParameters);
            BVSEOURL.InnerHtml = bvOutput.getUrl();
        }
    }
}