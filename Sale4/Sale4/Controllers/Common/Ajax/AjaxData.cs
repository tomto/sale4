using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Sale4.Controllers.Common
{

    public class AjaxData : JsonResult
    {
        public AjaxData()
        {
        }

        #region 构造函数
        public AjaxData(int code = 1, string message = "操作成功", object data = null)
        {
            Data = new AjaxDataModel()
            {
                Code = code,
                Message = message,
                Data = data,
                IsSuccess = code > 0
            };
        }

        public AjaxData(object data)
        {
            Data = new AjaxDataModel()
            {
                Code = 1,
                Message = "操作成功",
                Data = data,
                IsSuccess = true
            };
        }
        public AjaxData(bool isSuccess = true,object data = null)
        {
            Data = new AjaxDataModel()
            {
                IsSuccess = isSuccess,
                Data =  data
            };

            if (isSuccess)
            {
                Data.Code = 1;
                Data.Message = "操作成功";
            }
            else
            {
                Data.Code = 0;
                Data.Message = "操作失败";
            }
        }
        #endregion

        internal AjaxDataModel Data { get; private set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (this.Data == null) return;
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";

            var jsonSetting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy/MM/dd HH:mm:ss",
                ContractResolver = new DefaultContractResolver()//new CamelCasePropertyNamesContractResolver()
            };

            response.Write(JsonConvert.SerializeObject(this.Data, jsonSetting));
        }
    }

    internal class AjaxDataModel
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public bool IsSuccess { get; set; }
    }
}