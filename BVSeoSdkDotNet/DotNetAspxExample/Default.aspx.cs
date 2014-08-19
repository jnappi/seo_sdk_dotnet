using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //BVConfiguration bvConfig = new BVSdkConfiguration();
            //bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            //bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            //bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            //bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");

            //Establish a new BVConfiguration.  Properties within this configuration are typically set in bvconfig.properties.
            //addProperty can be used to override configurations set in bvconfig.properties.
            BVConfiguration _bvConfig = new BVSdkConfiguration();
            _bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch
            _bvConfig.addProperty(BVClientConfig.BOT_DETECTION, "false"); // set to true if user agent/bot detection is desired

            //this SDK supports retrieval of SEO contents from the cloud or local file system
            _bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            _bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            //_bvConfig.addProperty(BVClientConfig.STAGING, "false");  // set to true for staging environment data
            _bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");

            //insert root folder with the value provided.
            //if multiple deployment zones/display codes are used for this implementation, use conditional logic to set the appropriate BV_ROOT_FOLDER
            //_bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "bodyglove-8e186f6e16e2d688784728b360df41c5");
            //_bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "Main_Site-en_US");
            //_bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "adobe-55d020998d7b4776fb0f9df49278083c"); // get this value from BV
            //_bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "8814"); //get this value from BV
            _bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "300000");
            _bvConfig.addProperty(BVClientConfig.CLOUD_KEY, "myshco-126b543c32d9079f120a575ece25bad6");
            _bvConfig.addProperty(BVClientConfig.STAGING, "true");
            _bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, "9344ia");


            BVUIContent uiContent = new BVManagedUIContent(_bvConfig);

            BVParameters bvParameters = new BVParameters();
            //bvParameters.BaseURI = "http://localhost:8080/sample/someproduct.jsp";
            //bvParameters.PageURI = "http://localhost:8080/sample/someproduct.jsp?bvpage=ctre/id50524/stp";
            bvParameters.BaseURI = Request.Url.ToString().Contains("?") ? Request.Url.ToString().Substring(0, Request.Url.ToString().IndexOf("?")) : Request.Url.ToString();
            bvParameters.PageURI = Request.Url.ToString();
            bvParameters.UserAgent = Request.UserAgent;

            bvParameters.ContentType = new BVContentType(BVContentType.QUESTIONSPAGE);
            bvParameters.SubjectType = new BVSubjectType(BVSubjectType.CATEGORY);
            bvParameters.SubjectId = "Retail";

            //bvParameters.BaseURI = Request.Url.AbsoluteUri;
            //bvParameters.PageURI = Request.Url.AbsoluteUri + "?bvrrp=8814/reviews/product/4/PR6.htm&bvreveal=debug";
            //bvParameters.PageURI = Request.Url.AbsoluteUri + "?bvpage=ctre/id50524/stp";

            String theUIContent = uiContent.getContent(bvParameters);
            BVRRContainer.InnerHtml = theUIContent;
        }
    }
}
