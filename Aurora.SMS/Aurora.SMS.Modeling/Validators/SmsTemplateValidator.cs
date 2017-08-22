using Aurora.SMS.Modeling.Interfaces;
using FluentValidation;

namespace Aurora.SMS.Modeling.Validators
{
    /// <summary>
    /// The validator class for the SmsTemplate
    /// </summary>
    public class SmsTemplateValidator : AbstractValidator<ISmsTemplateModel>
    {
        public SmsTemplateValidator()
        {
            RuleFor(t => t.Name).NotEmpty().Length(5, 50);
            RuleFor(t => t.Description).NotEmpty().Length(5, 255);
            RuleFor(t => t.Text).NotEmpty();
        }
    }
}