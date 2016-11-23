using System.Web.Mvc;
using System.Web.Routing;

namespace Sale4.Controllers.API
{
    public class BaseApiController : Controller
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

        public JsonResult GetJsonResult(object data)
        {
            return Json(new { data = data }, JsonRequestBehavior.AllowGet);
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