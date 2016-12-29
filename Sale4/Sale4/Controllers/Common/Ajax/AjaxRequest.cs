using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sale4.Controllers.Common
{
    public class AjaxRequest : System.Web.Mvc.Controller
    {
        protected JsonResult GetJsonResult(object data = null)
        {
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        protected JsonResult JsonSuccess(object data = null)
        {
            return new AjaxData(data);
        }
        protected JsonResult JsonFail(string message = "操作失败")
        {
            return new AjaxData(0, message);
        }
        protected JsonResult JsonResult(int code = 1, string message = "操作失败", object data = null)
        {
            return new AjaxData(code, message, data);
        }
    }
}