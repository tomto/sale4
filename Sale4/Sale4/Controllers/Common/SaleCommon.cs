using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sale4.Controllers.Common
{
    public static class SaleCommon
    {
        public static string SiteHost = ConfigurationManager.AppSettings["SiteHost"];

        public static string ImgUrl = ConfigurationManager.AppSettings["ImgUrl"];
        
    }
}