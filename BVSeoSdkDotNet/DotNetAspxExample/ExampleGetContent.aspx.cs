using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;

namespace DotNetAspxExample
{
    public partial class ExampleGetContent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ContentType.SelectedIndex < 0)
                {
                    int selectedIndex = 0;
                    // Load contentType from session if it exists
                    if (Session["contentType:SelectedIndex"] != null)
                    {
                        selectedIndex = (Int32)Session["contentType:SelectedIndex"];
                    }
                    ContentType.SelectedIndex = selectedIndex;
                }
                ContentType.Attributes.Add("onchange", "savePageUrl();");
                loadData(
                    Request.Url.ToString(),
                    BVContentType.ctFromBVStateKeyword(ContentType.SelectedValue)
                );
            }
            else
            {
                // Special handling for hashfragment since it doesn't call ContentType_Changed on page refresh
                if (pageUrl.Value.Contains("#!"))
                {
                    loadData(
                       pageUrl.Value,
                        BVContentType.ctFromBVStateKeyword(ContentType.SelectedValue)
                    );
                }
            }
            // Save selected contentType to session
            Session["contentType:SelectedIndex"] = ContentType.SelectedIndex;
        }
        
        protected void ContentType_Changed(object sender, EventArgs e)
        {
            if (ContentType.SelectedIndex >= 0)
            {
                loadData(
                    (pageUrl.Value.Length > 0)?  pageUrl.Value : Request.Url.ToString(),
                    BVContentType.ctFromBVStateKeyword(ContentType.SelectedValue)
                );
            }
        }
        private void loadData(String currentUrl, String contentType)
        {
            String cloudKey = Request.QueryString["cloudkey"];
            String staging = Request.QueryString["staging"];
            String testing = Request.QueryString["testing"];
            String rootFolder = Request.QueryString["site"];
            String subjectType = Request.QueryString["subjecttype"];
            String subjectId = Request.QueryString["subjectid"];

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
                UserAgent = Request.UserAgent,
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
                BVRRSummaryContainer.InnerHtml = bvOutput.getAggregateRating(bvParameters);
                BVRRContainer.InnerHtml = bvOutput.getReviews(bvParameters);
                BVContent.InnerHtml = "";
            }
            else
            {
                BVContent.InnerHtml = bvOutput.getContent(bvParameters);
                BVRRSummaryContainer.InnerHtml = "";
                BVRRContainer.InnerHtml = "";
            }
        }
    }
}