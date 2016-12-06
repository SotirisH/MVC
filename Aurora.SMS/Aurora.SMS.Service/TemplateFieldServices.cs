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
        IEnumerable<EFModel.TemplateField> GetAllTemplateFields();
    }

    public class TemplateFieldServices : UnitOfWorkService<SMSDb>, ITemplateFieldServices
    {

        private readonly GenericRepository<EFModel.TemplateField, SMSDb> _templateFieldRepository;

        public TemplateFieldServices(IUnitOfWork<SMSDb> unitOfWork):base(unitOfWork)
        {
            _templateFieldRepository = _unitOfWork.GetGenericRepositoryOf<EFModel.TemplateField>();
        }

        public IEnumerable<EFModel.TemplateField> GetAllTemplateFields()
        {
            return _templateFieldRepository.GetAsQueryable().OrderBy(x => x.GroupName).OrderBy(x => x.Name).ToArray();
        }
    }
}
