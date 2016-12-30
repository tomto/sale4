using System.Web.Mvc;
using System.Web.Routing;
using Sale4.Controllers.Common;
using Utility.Ajax;

namespace Sale4.Controllers
{
    public class BaseController : AjaxRequest 
    {
        public bool IsLogin
        {
            get { return !string.IsNullOrWhiteSpace(UserSession); }
        }

        public string UserSession
        {
            get
            {
                return Session["UserSession"] == null ? "" : Session["UserSession"].ToString();
            }
        }

        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrWhiteSpace(UserSession))
            {
                filterContext.HttpContext.Response.Headers.Add("seesion", "timeout");
                //filterContext.HttpContext.Response.End();
            }

            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
        }
    }


}