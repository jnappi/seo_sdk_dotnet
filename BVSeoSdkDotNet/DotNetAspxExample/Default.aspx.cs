using System;
using System.Web.UI;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            bvConfig.addProperty(BVClientConfig.STAGING, "false");  // set to true for staging environment data
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            BVUIContent uiContent = new BVManagedUIContent(bvConfig);

            var bvParameters = new BVParameters
            {
                UserAgent = "google",
                BaseURI = Request.Url.AbsoluteUri,
                PageURI = Request.Url.AbsoluteUri + "?bvpage=ctre/id50524/stp"
            };

            var theUiContent = uiContent.getContent(bvParameters);
            BVRRContainer.InnerHtml = theUiContent;
        }
    }
}