using Aurora.Core.Data;
using Aurora.SMS.Service.Data;
using Aurora.SMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service
{
    public interface ITemplateServices
    {
        void Update(TemplateDTO template);
        bool IsTemplateUsed(int templateId);
        void DeleteTemplate(int templateId);
        void CreateTemplate(TemplateDTO template);
    }

    public class TemplateServices:  UnitOfWorkService<SMSDb>, ITemplateServices
    {
        private GenericRepository<EFModel.Template, SMSDb> _templateRepository;
        public TemplateServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateRepository = new GenericRepository<EFModel.Template, SMSDb>(_unitOfWork.DbFactory);
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap< TemplateDTO, EFModel.Template>());
        }

        public void CreateTemplate(TemplateDTO template)
        {
            var efTemplate = AutoMapper.Mapper.Map<EFModel.Template>(template);
            _templateRepository.Add(efTemplate);
        }

        public void DeleteTemplate(int templateId)
        {
            _templateRepository.Delete(templateId);
        }

        public bool IsTemplateUsed(int templateId)
        {
            return DbContext.SMSHistoryRecords.Any(h => h.TemplateId == templateId);
        }

        /// <summary>
        /// Updates a template or creates a new one if the
        /// modifies template has a reference to the SMSHostory
        /// </summary>
        /// <param name="template"></param>
        public void Update(TemplateDTO template)
        {
           
            var efTemplate = AutoMapper.Mapper.Map<EFModel.Template>(template);
            // check if there is any reference
            if (DbContext.SMSHistoryRecords.Any(h => h.TemplateId == template.Id))
            {
                efTemplate.Id = 0;
                _templateRepository.Add(efTemplate);
            }
            else
            {
                _templateRepository.Update(efTemplate);
            }
        }
    }
}
