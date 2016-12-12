using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models.SmsTemplate
{
    /// <summary>
    /// The validator class for the SmsTemplate
    /// </summary>
    public class SmsTemplateValidator: AbstractValidator<SmsTemplateViewModel>
    {
        public SmsTemplateValidator()
        {
            RuleFor(t => t.Name).NotEmpty().Length(5, 50);
            RuleFor(t => t.Description).NotEmpty().Length(5, 255);
            RuleFor(t => t.Text).NotEmpty();
        }
    }
}