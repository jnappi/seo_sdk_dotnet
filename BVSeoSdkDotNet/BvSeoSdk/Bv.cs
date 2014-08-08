using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BVSeoSdkDotNet.Config;
using BVSeoSdkDotNet.Content;
using BVSeoSdkDotNet.Model;


namespace BvSeoSdk
{
    public class Bv
    {
        private string _deploymentZoneId;
        private string _productId;
        private string _pageUrl;
        private string _seoKey;
        private string _bvProduct;
        private string _userAgent;
        private bool _staging;
        private bool _hostedDisplay;
        private bool _botDetection;
        private bool _includeDisplayIntegrationCode;
        private int _timeoutMs;
        private string _botRegexString;
        private string _internalFilePath;
        private string _productOrCategory;
        private string _commentStub = "<!--BVSEO|dz:{0}|sdk:v{1}-n|msg:{2} -->";
        private string _exception = "no exceptions";
        private string _version;
        private string _bvUrl = "";

        public Bv(String deploymentZoneID,
            String product_id,
            String cloudKey,
            String bv_product,
            bool staging = true,
            bool hosted_display = false,
            int timeout_ms = 1000,
            String bot_regex_string = "(msnbot|google|teoma|bingbot|yandexbot|yahoo)",
            bool bot_detection = true,
            bool includeDisplayIntegrationCode = false,
            String internalFilePath = "",
            String user_agent = "",
            String page_url = "",
            String product_or_category = "product"
            )
        {
            _botRegexString = bot_regex_string;
            _timeoutMs = timeout_ms;
            _hostedDisplay = hosted_display;
            _staging = staging;
            _userAgent = user_agent;
            _bvProduct = bv_product;
            _seoKey = cloudKey;
            _pageUrl = page_url;
            _productId = product_id;
            _deploymentZoneId = deploymentZoneID;
            _productOrCategory = product_or_category;
            _botRegexString = bot_regex_string;
            _botDetection = bot_detection;
            _includeDisplayIntegrationCode = includeDisplayIntegrationCode;
            _internalFilePath = internalFilePath;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public string ProductOrCategory
        {
            get { return _productOrCategory; }
            set { _productOrCategory = value; }
        }

        public String getSeoWithSdk(HttpRequest request)
        {
            BVConfiguration bvConfig = new BVSdkConfiguration();
            bvConfig.addProperty(BVClientConfig.CLOUD_KEY, _seoKey);
            bvConfig.addProperty(BVClientConfig.BV_ROOT_FOLDER, _deploymentZoneId);

            if (_botDetection)
            {
                bvConfig.addProperty(BVClientConfig.BOT_DETECTION, "true");
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.BOT_DETECTION, "false");
            }

            if (!string.IsNullOrEmpty(_botRegexString))
            {
                bvConfig.addProperty(BVClientConfig.CRAWLER_AGENT_PATTERN, _botRegexString);
            }

            bvConfig.addProperty(BVClientConfig.EXECUTION_TIMEOUT, _timeoutMs.ToString());

            if (_staging)
            {
                bvConfig.addProperty(BVClientConfig.STAGING, "true");
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.STAGING, "false");
            }

            if (_includeDisplayIntegrationCode)
            {
                bvConfig.addProperty(BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE, "true");
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.INCLUDE_DISPLAY_INTEGRATION_CODE, "false");
            }

            if (!string.IsNullOrEmpty(_internalFilePath))
            {
                bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "true");
                bvConfig.addProperty(BVClientConfig.LOCAL_SEO_FILE_ROOT, _internalFilePath);
            }
            else
            {
                bvConfig.addProperty(BVClientConfig.LOAD_SEO_FILES_LOCALLY, "false");
            }

            String subjectID = _productId;
            
            if(string.IsNullOrEmpty(_pageUrl))
                _pageUrl = request.Url.ToString();

            if (String.IsNullOrEmpty(_userAgent))
                _userAgent = request.UserAgent;

            BVContentType _contentType = new BVContentType(BVContentType.REVIEWS);
            if (!string.IsNullOrEmpty(_bvProduct))
            {
                switch (_bvProduct)
                {
                    case "reviews":
                        _contentType = new BVContentType(BVContentType.REVIEWS);
                        break;
                    case "questions":
                        _contentType = new BVContentType(BVContentType.QUESTIONS);
                        break;
                    case "stories":
                        _contentType = new BVContentType(BVContentType.STORIES);
                        break;
                }
            }

            BVSubjectType _subjectType = new BVSubjectType(BVSubjectType.PRODUCT);
            if (!string.IsNullOrEmpty(_productOrCategory))
            {
                switch (_productOrCategory)
                {
                    case "product":
                        _subjectType = new BVSubjectType(BVSubjectType.PRODUCT);
                        break;
                    case "category":
                        _subjectType = new BVSubjectType(BVSubjectType.CATEGORY);
                        break;
                    case "entry":
                        _subjectType = new BVSubjectType(BVSubjectType.ENTRY);
                        break;
                    case "detail":
                        _subjectType = new BVSubjectType(BVSubjectType.DETAIL);
                        break;
                }
            }

            BVParameters bvParam = new BVParameters();
            bvParam.UserAgent = _userAgent;
            bvParam.BaseURI = _pageUrl.Contains("?") ? _pageUrl.Substring(0, _pageUrl.IndexOf("?")) : _pageUrl;
            bvParam.PageURI = _pageUrl;
            bvParam.ContentType = _contentType;
            bvParam.SubjectType = _subjectType;
            bvParam.SubjectId = subjectID;

            BVUIContent bvOutput = new BVManagedUIContent(bvConfig);
            String outputContent = bvOutput.getContent(bvParam);

            return outputContent; 
        }
    }
}
