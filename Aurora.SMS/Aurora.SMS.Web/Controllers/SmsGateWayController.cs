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
            // if the DefaultSmsGateWayName45 has not been set the the first item is set as the DefaultSmsGateWayName45
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
                tmp.SiteUrl = item.Url;
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
        /// Sets this proxy as default
        /// </summary>
        /// <param name="model">Generic Post load</param>
        [HttpPost]
        public RedirectToRouteResult SetDefault(FormCollection model)
        {
            // Update cookie
            var cookie = Response.Cookies["DefaultSmsGateWayName45"];
            cookie.Value = model["proxyname"];
            Response.Cookies.Set(cookie);
            return RedirectToAction("Change");
        }
        
        [HttpPost]
        public ActionResult Save(string proxyName,string userName,string password)
        {
            try
            {
                var efproxy = _smsServices.GetAllProviders().First(m => m.Name == proxyName);
                // update only username & password
                efproxy.UserName = userName;
                efproxy.PassWord = password;
                _smsServices.SaveProxy(efproxy);
                var availableCredits = _smsServices.GetAvailableCredits(proxyName);
                return Json(new { Error = false, Message = availableCredits });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true , Message =ex.Message});
            }

        }

        public ActionResult TestProxy(string proxyName, string userName, string password)
        {
            try
            {
                var availableCredits = _smsServices.GetAvailableCredits(proxyName,userName, password);
                return Json(new { Error = false, Message = availableCredits });
            }
            catch (Exception ex)
            {
                return Json(new { Error = true, Message = ex.Message });
            }
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
            HttpCookie cookie= Request.Cookies["DefaultSmsGateWayName45"];
            if (cookie==null)
            {
                cookie = new HttpCookie("DefaultSmsGateWayName45");
                cookie.Expires = DateTime.MaxValue;
                cookie.Value = "SnailAbroad";
                Request.Cookies.Set(cookie);
            }
            return cookie;
        }
    }
}