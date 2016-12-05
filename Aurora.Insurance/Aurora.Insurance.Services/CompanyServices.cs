using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Insurance.EFModel;

namespace Aurora.Insurance.Services
{
    public interface ICompanyServices
    {
        IEnumerable<EFModel.Company> GetAll();
    }

    public class CompanyServices : ICompanyServices
    {
        public IEnumerable<Company> GetAll()
        {
            using (var db = new InsuranceDb())
            {
                return db.Companies.OrderBy(m=>m.Description).ToArray();
            }
        }
    }
}
