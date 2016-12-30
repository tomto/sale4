using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sale4
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Login",
                "Login",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "actIndex",
                "actIndex",
                new { action = "Index", controller = "Activity", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "actDetail",
                "actDetail",
                new { action = "Detail", controller = "Activity", id = UrlParameter.Optional }
            );

            #region API


            routes.MapRoute(
                "GetStatics",
                "API/GetStatics",
                new { action = "GetStatics", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteStatics",
                "API/DeleteStatics",
                new { action = "DeleteStatics", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "DeleteStaticsDetail",
                "API/DeleteStaticsDetail",
                new { action = "DeleteStaticsDetail", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "GetDetail",
                "API/GetDetail",
                new { action = "GetDetail", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "GetStaticsDetails",
                "API/GetStaticsDetails",
                new { action = "GetStaticsDetails", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "GetStaticsDetail",
                "API/GetStaticsDetail",
                new { action = "GetStaticsDetail", controller = "ActivityApi", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "SaveStaticsHtml",
                "API/SaveStaticsHtml",
                new { action = "SaveStaticsHtml", controller = "ActivityApi", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "SaveStaticsDetail",
                "API/SaveStaticsDetail",
                new { action = "SaveStaticsDetail", controller = "ActivityApi", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "CopyHtml",
                "API/CopyHtml",
                new { action = "CopyHtml", controller = "ActivityApi", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                "Upload",
                "Upload",
                new { action = "Index", controller = "Upload", url = UrlParameter.Optional }
            );
            

            #endregion API
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
