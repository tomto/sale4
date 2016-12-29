using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sale4.Controllers.Common;

namespace Sale4.Controllers.API
{
    public class UploadController : BaseController
    {
        // GET: Upload
        public JsonResult Index()
        {
            var appclient = new WebClient();
            var files = this.Request.Files;
            if (files.Count > 0)
            {
                try
                {
                    var name = Guid.NewGuid() + DateTime.Now.ToString("yyyyMMdd");
                    var file = this.Server.MapPath("~/Content/UploadFile/images/" + name);
                    files[0].SaveAs(file);
                    return JsonResult(1,file + "上传成功", SaleCommon.ImgUrl + name);
                }
                catch (Exception ex)
                {
                    return JsonFail(ex.StackTrace);
                }
            }
            return JsonFail("上传失败");
        }
    }
}