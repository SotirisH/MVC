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
        void Update(EFModel.Template template);
        /// <summary>
        /// Checks if the template has references to the SMS history table
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        bool IsTemplateUsed(int templateId);
        void DeleteTemplate(int templateId);
        void CreateTemplate(EFModel.Template template);
        IEnumerable<EFModel.Template> GetAll();
        EFModel.Template GetById(int id);
    }

    public class TemplateServices:  UnitOfWorkService<SMSDb>, ITemplateServices
    {
        private readonly GenericRepository<EFModel.Template, SMSDb> _templateRepository;
        
        public TemplateServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateRepository = _unitOfWork.GetGenericRepositoryOf<EFModel.Template>();
        }

        public void CreateTemplate(EFModel.Template template)
        {
            _templateRepository.Add(template);
            _unitOfWork.Commit();
        }

        public void DeleteTemplate(int templateId)
        {
            _templateRepository.Delete(templateId);
        }

        public IEnumerable<EFModel.Template> GetAll()
        {
            return _templateRepository.GetAll();
        }

        public EFModel.Template GetById(int id)
        {
            return  _templateRepository.GetById(id);
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
        public void Update(EFModel.Template template)
        {
            // check if there is any reference
            if (DbContext.SMSHistoryRecords.Any(h => h.TemplateId == template.Id))
            {
                template.Id = 0;
                _templateRepository.Add(template);
            }
            else
            {
                _templateRepository.Update(template);
            }
        }
    }
}
