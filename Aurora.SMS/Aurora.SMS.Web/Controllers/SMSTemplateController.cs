using Aurora.SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Controllers
{
    public class SMSTemplateController : Controller
    {
        private readonly ITemplateServices templateServices;
        private readonly ITemplateFieldServices templateFieldServices;

        public SMSTemplateController(ITemplateServices templateServices,
                                    ITemplateFieldServices templateFieldServices)
        {
            this.templateServices = templateServices;
            this.templateFieldServices = templateFieldServices;
        }

        // GET: SMSTemplate
        public ViewResult Index()
        {
            return View(templateServices.GetAll());
        }
        public ViewResult CreateEdit()
        {
            return View("CreateEdit", new EFModel.Template());
        }

        [HttpPost]
        public ActionResult CreateEdit(EFModel.Template template)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }
            if (template.Id != 0)
            {
                templateServices.Update(template);
            }
            else
            {
                templateServices.CreateTemplate(template);
            }
            return RedirectToAction("Index");
            
        }

        /// <summary>
        /// Dispays the form by injecting a new template object
        /// </summary>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("CreateEdit", new EFModel.Template());
        }

        public ViewResult Edit(int id)
        {
            return View("CreateEdit", templateServices.GetById(id));
        }

        public ViewResult GetTemplateFields()
        {
            return View("TemplateFields", templateFieldServices.GetAllTemplateFields());
        }
    }
}