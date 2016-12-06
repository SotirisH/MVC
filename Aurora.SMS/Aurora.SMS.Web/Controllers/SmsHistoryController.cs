using Aurora.SMS.EFModel;
using Aurora.SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Controllers
{
    public class SmsHistoryController : Controller
    {
        private readonly ISMSServices _smsServices;
        public SmsHistoryController(ISMSServices smsServices)
        {
            _smsServices = smsServices;
        }
        // GET: SmsHistory
        public ActionResult Index()
        {
            return View(new Aurora.SMS.Web.Models.SmsHistory.SmsHistoryViewModel());
        }
        [HttpPost]
        public ActionResult Index(Aurora.SMS.Web.Models.SmsHistory.SmsHistoryViewModel vm)
        {
            vm.HistoryResults = _smsServices.GetHistory(vm.Criteria);
            return View(vm);
        }
    }
}