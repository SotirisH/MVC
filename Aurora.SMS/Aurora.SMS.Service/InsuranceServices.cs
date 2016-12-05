using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service
{
    public interface IInsuranceServices
    {
        IEnumerable<Insurance.EFModel.Company> GetAllCompanies();
        IEnumerable<ContractDTO> GetContracts(QueryCriteriaDTO criteria);
    }

    public class InsuranceServices : IInsuranceServices
    {
        public IEnumerable<Insurance.EFModel.Company> GetAllCompanies()
        {
            var companyService = new Insurance.Services.CompanyServices();
            return companyService.GetAll();
        }

        public IEnumerable<ContractDTO> GetContracts(QueryCriteriaDTO criteria)
        {
            var contractServices = new Insurance.Services.ContractServices();
            return contractServices.GetContracts(criteria);
        }
    }
}
