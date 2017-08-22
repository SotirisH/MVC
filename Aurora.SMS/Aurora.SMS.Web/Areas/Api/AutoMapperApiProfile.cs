using Aurora.SMS.Web.Areas.Api.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Areas.Api
{
    public class AutoMapperApiProfile : Profile
    {
        public AutoMapperApiProfile()
        {
            CreateMap<EFModel.Template, SmsTemplateModel>()
                    .ForMember(dest => dest.RowVersion,
                    opt => opt.MapFrom(src => src.RowVersion == null ? null : Convert.ToBase64String(src.RowVersion)))
                .ReverseMap()
                    .ForMember(dest => dest.RowVersion,
                    opt => opt.MapFrom(src => src.RowVersion == null ? null : Convert.FromBase64String(src.RowVersion)));

        }
    }
}