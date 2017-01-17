using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aurora.SMS.Web.Models.SmsTemplate
{
    public class SmsTemplateViewModel: EntityBase, IValidatableObject
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
        [AllowHtml]
        public string Text { get; set; }
        /// <summary>
        /// The template object is semi-immutable.
        /// When a template is modified and been used in the SMS history
        /// then a new template record is created and the current marked as inactive
        /// </summary>
        public bool IsInactive { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new SmsTemplateValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}