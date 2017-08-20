using AutoMapper;

namespace Aurora.SMS.Web.App_Start
{
    /// <summary>
    /// Automaper configurations
    /// </summary>
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile()
        {
            CreateMap<EFModel.Template, Models.SmsTemplate.SmsTemplateViewModel>();
            CreateMap<Models.SmsTemplate.SmsTemplateViewModel, EFModel.Template>();
        }
    }
}