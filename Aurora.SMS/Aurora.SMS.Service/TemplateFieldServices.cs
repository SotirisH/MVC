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
    public interface ITemplateFieldServices
    {
        /// <summary>
        /// Return all fileds with a specific order
        /// </summary>
        /// <returns></returns>
        IEnumerable<TemplateFieldDTO> GetAllTemplateFields();
    }

    public class TemplateFieldServices : UnitOfWorkService<SMSDb>, ITemplateFieldServices
    {

        private readonly GenericRepository<EFModel.TemplateField, SMSDb> _templateFieldRepository;

        public TemplateFieldServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateFieldRepository = new GenericRepository<EFModel.TemplateField, SMSDb>(_unitOfWork.DbFactory);
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<TemplateFieldDTO, EFModel.TemplateField>());
        }

        public IEnumerable<TemplateFieldDTO> GetAllTemplateFields()
        {
            return AutoMapper.Mapper.Map <IEnumerable<TemplateFieldDTO>> (_templateFieldRepository.GetAsQueryable().OrderBy(x => x.GroupName).OrderBy(x => x.Name).ToArray());
        }
    }
}
