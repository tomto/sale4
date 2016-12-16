using System.Web.Mvc;
using Sale4.Controllers.API;

namespace Sale4.Controllers
{
    public class HomeController : BaseController
    {
        

        public ActionResult Index()
        {
            if (IsLogin)
            {
                Response.Redirect("/actindex");
            }

            return View();
        }

        public class User
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public JsonResult Login(User user)
        {
            var call = new CallResult();
            if (!string.IsNullOrWhiteSpace(user.UserName) && !string.IsNullOrWhiteSpace(user.Password))
            {
                call = SignIn(user);
            }


            return GetJsonResult(new { data = call });

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
            return GetJsonResult(new { data = "/Home" });
        }
    }
}