using Aurora.SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Controllers
{
    /// <summary>
    /// Defines all the actions related to the SMS Provider Proxy
    /// </summary>
    public class SmsGateWayController : Controller
    {
     
        private readonly ISMSServices _smsServices;
        public SmsGateWayController(ISMSServices smsServices)
        {
            _smsServices = smsServices;
        }
        public ViewResult Change()
        {
            var vw = new Models.SmsGateway.SmsGateWayViewModel();
            vw.SmsGateWayList = _smsServices.GetAllProviders().ToList();
            var cookie = GetDefaultSmsGateWayCookie();
            var smsDefaultGateWayName = cookie.Value;
            // if the DefaultSmsGateWayName has not been set the the first item is set as the DefaultSmsGateWayName
            if (string.IsNullOrWhiteSpace(smsDefaultGateWayName))
            {
                if(vw.SmsGateWayList.Any())
                {
                    smsDefaultGateWayName = vw.SmsGateWayList.First().Name;
                }
            }
            vw.DefaultSmsGateWay = smsDefaultGateWayName;
            cookie.Value = smsDefaultGateWayName;
            cookie.Expires = DateTime.MaxValue;
            Request.Cookies.Set(cookie);
            return View(vw);
        }

        public PartialViewResult SmsGateWayBlockView()
        {
            var smsGateWayName = Response.Cookies["DefaultSmsGateWayName"].Value;
            ViewBag.SmsGateWayName = smsGateWayName;
            return PartialView("_SmsGateWay");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Generic Post load</param>
        [HttpPost]
        public void SetDefault(FormCollection model)
        {
            // Set default on cookie
            var cookie = GetDefaultSmsGateWayCookie();
            cookie.Value = model["smsGateWayProxy.Name"];
            Request.Cookies.Set(cookie);
        }
        public ViewResult SetDefault()
        {

            return null;
        }



        private HttpCookie GetDefaultSmsGateWayCookie()
        {
            HttpCookie cookie= Request.Cookies["DefaultSmsGateWayName"];
            if (cookie==null)
            {
                cookie = new HttpCookie("DefaultSmsGateWayName");
                Request.Cookies.Set(cookie);
            }
            return cookie;
        }
    }
}