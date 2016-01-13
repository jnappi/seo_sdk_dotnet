using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    /**
     * Loads multiple bv content on the page based on values in
     * ContentType1 and ContentType2.
     * Note: Same ContentType is not supported twice. Hence, ContentType2
     * will be automatically adjusted when both the content types are same
     */
    public partial class ExampleGetMultipleContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ContentType1.SelectedIndex < 0)
                {
                    int selectedIndex = 0;
                    // Load contentType from session if it exists
                    if (Session["contentType1:SelectedIndex"] != null)
                    {
                        selectedIndex = (Int32)Session["contentType1:SelectedIndex"];
                    }
                    ContentType1.SelectedIndex = selectedIndex;
                }
                if (ContentType2.SelectedIndex < 0)
                {
                    int selectedIndex = 0;
                    // Load contentType from session if it exists
                    if (Session["contentType2:SelectedIndex"] != null)
                    {
                        selectedIndex = (Int32)Session["contentType2:SelectedIndex"];
                    }
                    ContentType2.SelectedIndex = selectedIndex;
                }
                AdjustSelectedIndexToBeNotEqual(ContentType1, ContentType2);
                ContentType1.Attributes.Add("onchange", "savePageUrl();");
                ContentType2.Attributes.Add("onchange", "savePageUrl();");
                loadData(Request.Url.ToString());
            }
            else
            {
                // Special handling for hashfragment since it doesn't call ContentType_Changed on page refresh
                if (pageUrl.Value.Contains("#!"))
                {
                    AdjustSelectedIndexToBeNotEqual(ContentType1, ContentType2);
                    loadData(pageUrl.Value);
                }
            }
            // Save selected contentType to session
            Session["contentType1:SelectedIndex"] = ContentType1.SelectedIndex;
            Session["contentType2:SelectedIndex"] = ContentType2.SelectedIndex;
        }

        private void AdjustSelectedIndexToBeNotEqual
        (
            System.Web.UI.WebControls.ListBox fixedControl,
            System.Web.UI.WebControls.ListBox toAlterControl
        )
        {
            int totalItems = toAlterControl.Items.Count;
            if (fixedControl.SelectedIndex == toAlterControl.SelectedIndex)
            {
                toAlterControl.SelectedIndex = (toAlterControl.SelectedIndex + 1) % totalItems;
            }
        }

        private void loadData(String baseUrl)
        {
            loadData(
                baseUrl,
                BVContentType.ctFromBVStateKeyword(ContentType1.SelectedValue),
                1,
                BVRRSummaryContainer1,
                BVRRContainer1,
                BVContent1
            );
            loadData(
                baseUrl,
                BVContentType.ctFromBVStateKeyword(ContentType2.SelectedValue),
                2,
                BVRRSummaryContainer2,
                BVRRContainer2,
                BVContent2
            );
        }

        protected void ContentType_Changed(object sender, EventArgs e)
        {
            if (ContentType1.SelectedIndex >= 0 && ContentType2.SelectedIndex >= 0)
            {
                AdjustSelectedIndexToBeNotEqual(ContentType1, ContentType2);
                loadData(pageUrl.Value);
            }
        }

        private void loadData(
            String currentUrl,
            String contentType,
            int index,
            HtmlGenericControl reviewSummaryControl,
            HtmlGenericControl reviewControl,
            HtmlGenericControl contentControl
        )
        {
            String cloudKey = Request.QueryString["cloudkey" + index];
            String staging = Request.QueryString["staging" + index];
            String testing = Request.QueryString["testing" + index];
            String rootFolder = Request.QueryString["site" + index];
            String subjectType = Request.QueryString["subjecttype" + index];
            String subjectId = Request.QueryString["subjectid" + index];

            if (subjectType != null)
            {
                subjectType = BVSubjectType.subjectType(subjectType);
            }
            // Separate defaulting Logic for spotlight vs non-spotlight content
            if (contentType.Equals(BVContentType.SPOTLIGHTS, StringComparison.OrdinalIgnoreCase))
            {
                if (subjectId == null)
                {
                    subjectId = "category-1";
                }
                if (cloudKey == null)
                {
                    cloudKey = "spotlight-four-746e2fc1211dc8964560350c9f28b67a";
                }
                if (staging == null)
                {
                    staging = "false";
                }
                if (testing == null)
                {
                    testing = "true";
                }
                if (rootFolder == null)
                {
                    rootFolder = "Main_Site-en_US";
                }
                if (subjectType == null)
                {
                    subjectType = BVSubjectType.CATEGORY;
                }
            }
            else if (contentType.Equals(BVContentType.REVIEWS, StringComparison.OrdinalIgnoreCase))
            {
                if (subjectId == null)
                {
                    subjectId = "product1";
                }
                if (cloudKey == null)
                {
                    cloudKey = "spotlight-five-311f5a3337b8d5e0d817adb7af279b0a";
                }
                if (staging == null)
                {
                    staging = "true";
                }
                if (testing == null)
                {
                    testing = "false";
                }
                if (rootFolder == null)
                {
                    rootFolder = "Other_Zone-en_US";
                }
                if (subjectType == null)
                {
                    subjectType = BVSubjectType.PRODUCT;
                }
            }
            else if (contentType.Equals(BVContentType.QUESTIONS, StringComparison.OrdinalIgnoreCase))
            {
                if (subjectId == null)
                {
                    subjectId = "data-gen-u2y505e9u1l65i43l6zz22ve6";
                }
                if (cloudKey == null)
                {
                    cloudKey = "agileville-78B2EF7DE83644CAB5F8C72F2D8C8491";
                }
                if (staging == null)
                {
                    staging = "true";
                }
                if (testing == null)
                {
                    testing = "false";
                }
                if (rootFolder == null)
                {
                    rootFolder = "Main_Site-en_US";
                }
                if (subjectType == null)
                {
                    subjectType = BVSubjectType.PRODUCT;
                }
            }
            else
            {
                if (subjectId == null)
                {
                    subjectId = "test1";
                }
                if (cloudKey == null)
                {
                    cloudKey = "Allergan-09b83694534c0d1bcd24851e9e9d172f";
                }
                if (staging == null)
                {
                    staging = "true";
                }
                if (testing == null)
                {
                    testing = "false";
                }
                if (rootFolder == null)
                {
                    rootFolder = "8183-en_us";
                }
                if (subjectType == null)
                {
                    subjectType = BVSubjectType.PRODUCT;
                }
            }

            // Setting up BVConfiguration and BVParameters
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, cloudKey);
            bvConfig.addProperty(BVClientConfig.STAGING, staging);
            bvConfig.addProperty(BVClientConfig.TESTING, testing);
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, rootFolder);
            bvConfig.addProperty(BVClientConfig.SEO_SDK_ENABLED, "true");  // use this as a kill switch
            bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false"); // set to false if using cloud-based content
            bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, "/");
            bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, "yandex");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, "1500");
            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT_BOT, "2000");

            var bvParameters = new BVParameters
            {
                BaseURI = currentUrl,
                PageURI = currentUrl,
                ContentType = new BVContentType(contentType),
                SubjectType = new BVSubjectType(subjectType),
                SubjectId = subjectId
            };
            if (contentType.Equals(BVContentType.STORIES, StringComparison.OrdinalIgnoreCase))
            {
                bvParameters.ContentSubType = new BVContentSubType(BVContentSubType.STORIES_LIST);
            }

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            if (contentType.Equals(BVContentType.REVIEWS, StringComparison.OrdinalIgnoreCase))
            {
                reviewSummaryControl.InnerHtml = bvOutput.getAggregateRating(bvParameters);
                reviewControl.InnerHtml = bvOutput.getReviews(bvParameters);
                contentControl.InnerHtml = "";
            }
            else
            {
                contentControl.InnerHtml = bvOutput.getContent(bvParameters);
                reviewSummaryControl.InnerHtml = "";
                reviewControl.InnerHtml = "";
            }
        }
    }
}