using System;
using System.Web.UI.HtmlControls;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace IntegrationTest.Subject_Content
{
    public partial class Product_Questions : System.Web.UI.Page
    {
        protected HtmlGenericControl bvrrSummaryContainer, bvrrReviewsContainer, bvrrContentContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            Helper helper = new Helper(Page.Request);
            BVSdkConfiguration config = helper.GetConfiguration("myshco-3e3001e88d9c32d19a17cafacb81bec7", "9344");
            BVParameters bvParams = helper.GetParams("5000001", "p", "qa");
            BVManagedUIContent uiContent = new BVManagedUIContent(config);
            string content = uiContent.getContent(bvParams);
            bvrrContentContainer.InnerHtml = content;
        }
    }
}