using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BvSeoSdk;

namespace DotNetMvcExample.Controllers
{
    public class HomeController : Controller
    {
        public String Index()
        {
            return new Bv(
                deploymentZoneID: "9344",
                product_id: "5000001", 
                //The page_url is optional
                //page_url: "http://www.example.com/store/products/data-gen-696yl2lg1kurmqxn88fqif5y2/",
                cloudKey: "myshco-3e3001e88d9c32d19a17cafacb81bec7", //agileville
                bv_product: BvProduct.REVIEWS, 
                //bot_detection: false, //by default bot_detection is set to true
                user_agent: "msnbot") //Setting user_agent for testing. Leave this blank in production.
                .getSeoWithSdk(System.Web.HttpContext.Current.Request);
        }

    }
}
