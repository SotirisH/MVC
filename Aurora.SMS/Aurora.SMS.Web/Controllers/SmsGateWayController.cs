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
            var vw = new Models.SmsGateWay.SmsGateWayViewModel();
            vw.SmsGateWayList = _smsServices.GetAllProviders().ToList();

            var smsGateWayName = Response.Cookies["DefaultSmsGateWayName"].Value;
            // if the DefaultSmsGateWayName has not been set the the first item is set as the DefaultSmsGateWayName
            if (string.IsNullOrWhiteSpace(smsGateWayName))
            {
                Response.Cookies.Set(new HttpCookie("DefaultSmsGateWayName", vw.SmsGateWayList.FirstOrDefault() == null ? null : vw.SmsGateWayList.First().Name));
                //throw new NotImplementedException();
            }
            return View(vw);
        }

        public PartialViewResult SmsGateWayBlockView()
        {
            var smsGateWayName = Response.Cookies["DefaultSmsGateWayName"].Value;
            ViewBag.SmsGateWayName = smsGateWayName;
            return PartialView("_SmsGateWay");
        }
 
    }
}