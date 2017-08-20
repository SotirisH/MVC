using Aurora.Core.Data;
using Aurora.SMS.Service;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Controllers
{
    public class SMSTemplateController : Controller
    {
        private readonly ITemplateServices templateServices;
        private readonly ITemplateFieldServices templateFieldServices;
        private readonly IUnitOfWork UoW;


        public SMSTemplateController(ITemplateServices templateServices,
                                    ITemplateFieldServices templateFieldServices,
                                    IUnitOfWork UoW)
        {
            this.templateServices = templateServices;
            this.templateFieldServices = templateFieldServices;
            this.UoW = UoW;
        }

        // GET: SMSTemplate
        public ViewResult Index()
        {
            return View(templateServices.GetAll());
        }
        [Obsolete("Only for UI test")]
        public ViewResult CreateEdit()
        {
            return View("CreateEdit", new Models.SmsTemplate.SmsTemplateViewModel());
        }

        [HttpPost]
        public ActionResult CreateEdit(Models.SmsTemplate.SmsTemplateViewModel vm)
        {

            // Demo using FluentValidation
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            //var regExp = "<div class=\"alert alert-dismissible alert-success\" contenteditable=\"false\" style=\"display:inline-block\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button><span>" + templateField.Name + "</span></div>";
            // strip any additional spaces that Js might add in the experssion
            vm.Text = Regex.Replace(vm.Text, "alert\\s*-\\s*dismissible", "alert-dismissible");
            vm.Text = Regex.Replace(vm.Text, "alert\\s*-\\s*success", "alert-success");
            vm.Text = Regex.Replace(vm.Text, "data\\s*-\\s*dismiss", "alert-dismiss");
            if (vm.Id != 0)
            {
                templateServices.Update(AutoMapper.Mapper.Map<EFModel.Template>(vm));
            }
            else
            {
                templateServices.CreateTemplate(AutoMapper.Mapper.Map<EFModel.Template>(vm));
            }
            UoW.Commit();

            return RedirectToAction("Index");

        }

        /// <summary>
        /// Dispays the form by injecting a new template object
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("CreateEdit", new Models.SmsTemplate.SmsTemplateViewModel());
        }

        public ViewResult Edit(int id)
        {

            return View("CreateEdit", AutoMapper.Mapper.Map<Models.SmsTemplate.SmsTemplateViewModel>(templateServices.GetById(id)));
        }

        public ViewResult GetTemplateFields()
        {
            return View("TemplateFields", templateFieldServices.GetAllTemplateFields());
        }
    }
}