using System;
using System.Web.UI.HtmlControls;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace IntegrationTest.Subject_Content
{
    public partial class Product_StoriesPage : System.Web.UI.Page
    {
        protected HtmlGenericControl bvrrSummaryContainer, bvrrReviewsContainer, bvrrContentContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            Helper helper = new Helper(Page.Request);
            BVSdkConfiguration config = helper.GetConfiguration("myshco-126b543c32d9079f120a575ece25bad6", "9344ia");
            BVParameters bvParams = helper.GetParams("5000001", "p", "sp");
            BVManagedUIContent uiContent = new BVManagedUIContent(config);
            config.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "12");
            bvParams.UserAgent = "google";
            bvParams.PageURI = Request.Url.ToString() + "?bvpage=ctsp/stp/pg2";
            string summary = uiContent.getAggregateRating(bvParams);
            string reviews = uiContent.getReviews(bvParams);
            string content = uiContent.getContent(bvParams);
            bvrrSummaryContainer.InnerHtml = summary;
            bvrrReviewsContainer.InnerHtml = reviews;
            bvrrContentContainer.InnerHtml = content;
        }
    }
}