using Aurora.SMS.Modeling.Validators;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Aurora.SMS.Web.Areas.Api.Models
{
    [Validator(typeof(SmsTemplateValidator))]
    public class SmsTemplateModel : EntityBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// The SMS template body
        /// </summary>
        /// <remarks>
        /// The property is decorated with the AllowHtml Attribute in order to avoid
        /// the Exception:A potentially dangerous Request.Form value was detected from the client
        /// </remarks>
        public string Text { get; set; }
        /// <summary>
        /// The template object is semi-immutable.
        /// When a template is modified and been used in the SMS history
        /// then a new template record is created and the current marked as inactive
        /// </summary>
        public bool IsInactive { get; set; }

    }
}