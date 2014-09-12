using System;
using System.Web.UI.HtmlControls;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace IntegrationTest.Subject_Content
{
    public partial class Product_ReviewsPage : System.Web.UI.Page
    {
        protected HtmlGenericControl bvrrSummaryContainer, bvrrReviewsContainer, bvrrContentContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            Helper helper = new Helper(Page.Request);
            BVSdkConfiguration config = helper.GetConfiguration("myshco-126b543c32d9079f120a575ece25bad6", "9344ia");
            BVParameters bvParams = helper.GetParams("5000001", "p", "rp");
            BVManagedUIContent uiContent = new BVManagedUIContent(config);
            string content = uiContent.getContent(bvParams);
            bvrrContentContainer.InnerHtml = content;
        }
    }
}