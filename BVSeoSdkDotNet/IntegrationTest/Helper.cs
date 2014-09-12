using System.Web;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Model;

namespace IntegrationTest.Subject_Content
{
    public class Helper
    {
        private readonly HttpRequest _request;

        public HttpRequest Request
        {
            get { return _request; }
        }

        public Helper (HttpRequest request)
        {
            _request = request;
        }

        public BVSdkConfiguration GetConfiguration(string cloudKey, string rootFolder, string useStaging = "true")
        {
            BVSdkConfiguration config = new BVSdkConfiguration();
            config.addProperty(BVClientConfig.CLOUD_KEY, cloudKey);
            config.addProperty(BVClientConfig.BV_ROOT_FOLDER, rootFolder);
            config.addProperty(BVClientConfig.STAGING, useStaging);
            return config;
        }

        public BVParameters GetParams(string subjectId, string subjectType, string contentType, string subType = "")
        {
            string pageUrl = Request.Url.ToString();

            BVParameters bvParams = new BVParameters
            {
                BaseURI = pageUrl.Contains("?") ? pageUrl.Substring(0, pageUrl.IndexOf("?")) : pageUrl,
                PageURI = Request.Url.ToString(),
                UserAgent = Request.UserAgent,
                SubjectId = subjectId,
                SubjectType = new BVSubjectType(subjectType),
                ContentType = new BVContentType(contentType),
            };

            if (subjectType != "")
                bvParams.ContentSubType = new BVContentSubType(subType);

            return bvParams;
        }
    }
}