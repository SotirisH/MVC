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

            var smsProxies = _smsServices.GetAllProviders();
            var cookie = GetDefaultSmsGateWayCookie();
            var smsDefaultGateWayName = cookie.Value;
            // if the DefaultSmsGateWayName has not been set the the first item is set as the DefaultSmsGateWayName
            if (string.IsNullOrWhiteSpace(smsDefaultGateWayName))
            {
                if (smsProxies.Any())
                {
                    smsDefaultGateWayName = smsProxies.First().Name;
                }
            }
            cookie.Value = smsDefaultGateWayName;
            Request.Cookies.Set(cookie);

            var vm = new List<Models.SmsGateway.SmsGatewayProxyViewModel>();
            foreach (var item in smsProxies)
            {
                Models.SmsGateway.SmsGatewayProxyViewModel tmp = new Models.SmsGateway.SmsGatewayProxyViewModel();
                tmp.LogoUrl = item.LogoUrl;
                tmp.Name = item.Name;
                tmp.IsDefault = (smsDefaultGateWayName == item.Name);
                tmp.UserName = item.UserName;
                tmp.Pasword = item.PassWord;
                vm.Add(tmp);
            }

            return View(vm);
        }

        public PartialViewResult SmsGateWayBlockView()
        {
            var smsGateWayName = GetDefaultSmsGateWayCookie().Value;
            ViewBag.SmsGateWayName = smsGateWayName;
            return PartialView("_SmsGateWay");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Generic Post load</param>
        [HttpPost]
        public RedirectToRouteResult SetDefault(FormCollection model)
        {
            // Update cookie
            var cookie = Response.Cookies["DefaultSmsGateWayName"];
            cookie.Value = model["Index"];
            Response.Cookies.Set(cookie);
            return RedirectToAction("Change");
        }

        /// <summary>
        /// Returns the available credits of the active SmsProxy
        /// </summary>
        /// <returns></returns>
        public string GetAvailableCredits()
        {
            var cookie = GetDefaultSmsGateWayCookie();
            string defaultSmsGateWayName = cookie.Value;
            if (!string.IsNullOrWhiteSpace(defaultSmsGateWayName))
            {
               return _smsServices.GetAvailableCredits(defaultSmsGateWayName);
            }
            return "?";
        }
        /// <summary>
        /// Retrieves or creates an HttpCookie where the nave 
        /// of the default SmsGateway is strored
        /// </summary>
        /// <returns></returns>
        private HttpCookie GetDefaultSmsGateWayCookie()
        {
            HttpCookie cookie= Request.Cookies["DefaultSmsGateWayName"];
            if (cookie==null)
            {
                cookie = new HttpCookie("DefaultSmsGateWayName");
                cookie.Expires = DateTime.MaxValue;
                Request.Cookies.Set(cookie);
            }
            return cookie;
        }
    }
}