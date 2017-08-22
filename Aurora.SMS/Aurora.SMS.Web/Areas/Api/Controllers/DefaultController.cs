using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Areas.Api.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Api/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}