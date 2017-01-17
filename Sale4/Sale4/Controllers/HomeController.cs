using System.Web.Mvc;
using Sale4.Controllers.API;
using Utility.Ajax;

namespace Sale4.Controllers
{
    public class HomeController : AjaxRequest
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(User user)
        {
            var call = new CallResult()
            {
                State = false
            };
            if (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password))
            {
                call = SignIn(user);
            }
            if (call.State)
            {
                return JsonSuccess(call);
            }
            
            return JsonFail(call.BMsg);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private CallResult SignIn(User user)
        {
            var call = new CallResult()
            {
                State =  false,
                BMsg = "账号/密码,错误"
            };
            if (user.UserName == "hujue" && user.Password== "123456")
            {
                call.State = true;
                call.BMsg = "登录成功";

                Session["UserSession"] = user.UserName;
            }

            return call;
        }
        

        /// <summary>
        /// 登陆
        /// </summary>
        /// <returns></returns>
        private JsonResult SignOut()
        {
            Session["UserSession"] = null;
            return JsonSuccess("/Home/Index");
        }
    }


    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}