using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sale4.Controllers.API;

namespace Sale.Controllers
{
    public class ActivityController : BaseApiController
    {
        // GET: Activity
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BasicInfo()
        {
            return View();
        }

        public ActionResult Detail()
        {
            return View();
        }


        public ActionResult FloorDetail()
        {
            return View();
        }

    }
}