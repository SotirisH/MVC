using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.Services
{
    public interface IContractServices
    {
        /// <summary>
        /// The main service that will return the contacs based on the given criteia
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<ContractDTO> GetCotracts(QueryCriteriaDTO criteria);
        /// <summary>
        /// The async version of the method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IEnumerable<ContractDTO>>  GetCotractsAsync(QueryCriteriaDTO criteria);
    }
    public class ContractServices : IContractServices
    {
        public IEnumerable<ContractDTO> GetCotracts(QueryCriteriaDTO criteria)
        {
            throw new NotImplementedException();
            //using (var db = new InsuranceDb())
            //{
            //    var query =  db.Contracts.Include;

                           

            //}
            
            
        }

        public async Task<IEnumerable<ContractDTO>> GetCotractsAsync(QueryCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}
