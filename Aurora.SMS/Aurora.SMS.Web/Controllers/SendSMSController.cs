using Aurora.SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Controllers
{
    public class SendSMSController : Controller
    {

        private readonly IInsuranceServices _insuranceServices;
        private readonly ITemplateServices _templateServices;
        private readonly ISMSServices _smsServices;

        public SendSMSController(IInsuranceServices insuranceServices,
                                ITemplateServices templateServices,
                                ISMSServices smsServices)
        {
            _insuranceServices = insuranceServices;
            _templateServices = templateServices;
            _smsServices = smsServices;
        }
        // GET: SendSMS
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult SelectCustomers()
        {
            var mock = new Models.CustomerSelectionViewModel();
            mock.Companies = _insuranceServices.GetAllCompanies();
            // If Contracts is null an Exception is thrown!!!
            mock.Contracts = new List<Insurance.Services.DTO.ContractDTO>();
            return View(mock);
        }

        [HttpPost]
        public ViewResult SelectCustomers(Models.CustomerSelectionViewModel vm)
        {
            var results = _insuranceServices.GetContracts(vm.Criteria);
            vm.Companies = _insuranceServices.GetAllCompanies();
            vm.Contracts = results;
            return View(vm);
        }


        [HttpPost]
        public ActionResult AdvancedToSelectTemplate(Models.CustomerSelectionViewModel vm)
        {
            // Store Criteria into the session
            SessionHelper.Current.Criteria = vm.Criteria;
            return RedirectToAction("SelectTemplate");
        }

        public ViewResult SelectTemplate()
        {
            var vm = new Models.SelectedTemplateViewModel();
            vm.Templates = _templateServices.GetAll();
            return View(vm);
        }

        [HttpPost]
        public ActionResult SelectTemplate(Models.SelectedTemplateViewModel vm)
        {
            SessionHelper.Current.SelectedTemplateId = vm.SelectedTemplateId;
            return RedirectToAction("Preview"); 
        }

        public ViewResult Preview()
        {
            Insurance.Services.DTO.QueryCriteriaDTO criteria = SessionHelper.Current.Criteria as Insurance.Services.DTO.QueryCriteriaDTO;
            int selectedTemplateId = SessionHelper.Current.SelectedTemplateId;

            var recepients =_insuranceServices.GetContracts(criteria);
            var previewMessages=_smsServices.ConstructSMSMessages(recepients, selectedTemplateId);
            return View(previewMessages);
        }

        public ViewResult BulkSmsResult()
        {
            Insurance.Services.DTO.QueryCriteriaDTO criteria = SessionHelper.Current.Criteria as Insurance.Services.DTO.QueryCriteriaDTO;
            int selectedTemplateId = SessionHelper.Current.SelectedTemplateId;
            var recepients = _insuranceServices.GetContracts(criteria);
            var previewMessages = _smsServices.ConstructSMSMessages(recepients, selectedTemplateId);
            var sessionId = _smsServices.SendBulkSMS(previewMessages, "aurorafakeprovider");
            ViewBag.SessionId = sessionId;
            return View();
        }

    }
}