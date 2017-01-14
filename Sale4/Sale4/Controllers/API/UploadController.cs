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
                    string extendName = Path.GetExtension(files[0].FileName);
                    string[] extendsName = { ".xls", ".doc", ".csv", ".docx", ".xlsx", ".rar", ".jpg", ".jpeg", ".png", ".ppt", ".pptx", ".pdf" };
                    if (!extendsName.Contains(extendName))
                    {
                        return JsonFail("文件格式不正确");
                    }

                    var name = Guid.NewGuid() + DateTime.Now.ToString("yyyyMMdd");
                    var file = this.Server.MapPath("~/Content/UploadFile/images/" + name + extendName);
                    files[0].SaveAs(file);
                    return JsonResult(1, "上传成功", SaleCommon.ImgUrl + name + extendName);
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