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
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "spotlight-four-746e2fc1211dc8964560350c9f28b67a");
            bvConfig.addProperty(BVClientConfig.STAGING, "false");
            bvConfig.addProperty(BVClientConfig.TESTING, "true");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");

            var bvParameters = new BVParameters
            {
                BaseURI =
                    Request.Url.ToString().Contains("?")
                        ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?"))
                        : Request.Url.ToString(),
                PageURI = Request.Url.ToString(),
                UserAgent = Request.UserAgent,
                ContentType = new BVContentType(BVContentType.SPOTLIGHTS),
                SubjectType = new BVSubjectType(BVSubjectType.CATEGORY),
                SubjectId = "category-1"
            };

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            BVRRContainer.InnerHtml = bvOutput.getContent(bvParameters);
            BVSEOURL.InnerHtml = bvOutput.getUrl();
            
        }
    }
}