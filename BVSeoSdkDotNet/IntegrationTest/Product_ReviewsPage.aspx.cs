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
            BVSdkConfiguration config = helper.GetConfiguration("myshco-69cb945801532dcfb57ad2b0d2471b68", "Main_Site-en_US");
            BVParameters bvParams = helper.GetParams("5000001", "p", "rp");
            BVManagedUIContent uiContent = new BVManagedUIContent(config);
            string content = uiContent.getContent(bvParams);
            bvrrContentContainer.InnerHtml = content;
        }
    }
}