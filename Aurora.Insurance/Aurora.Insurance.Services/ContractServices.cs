using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Aurora.Insurance.Services
{
    public interface IContractServices
    {
        /// <summary>
        /// The main service that will return the contacs based on the given criteia
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        IEnumerable<ContractDTO> GetContracts(QueryCriteriaDTO criteria);
        /// <summary>
        /// The async version of the method
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IEnumerable<ContractDTO>>  GetCotractsAsync(QueryCriteriaDTO criteria);
    }
    public class ContractServices : IContractServices
    {
        public IEnumerable<ContractDTO> GetContracts(QueryCriteriaDTO criteria)
        {
            //https://blogs.msdn.microsoft.com/meek/2008/05/02/linq-to-entities-combining-predicates/
            //http://www.codeproject.com/Articles/1079028/Build-Lambda-Expressions-Dynamically
            //https://msdn.microsoft.com/en-us/library/mt654267.aspx
            using (var db = new InsuranceDb())
            {

                // create a dynamic query

                // this is in case the list of statements is empty and we setup by using Expression.Constant(true). Cannot use null
                IQueryable<EFModel.Contract> queryableData = db.Contracts;

                Expression finalExpression= Expression.Constant(true); 
                Expression<Func<EFModel.Contract, bool>> contractExp=null;
                var query = db.Contracts.Include(c=>c.Person) ;
                if (!string.IsNullOrWhiteSpace(criteria.ContractNumber))
                {
                    contractExp = contract => contract.ContractNumber == criteria.ContractNumber;
                    finalExpression = Expression.AndAlso( finalExpression , contractExp.Body );
                }
                if (!string.IsNullOrWhiteSpace(criteria.PlateNumber))
                {
                    contractExp = c => c.PlateNumber == criteria.PlateNumber;
                    finalExpression = Expression.AndAlso(finalExpression, contractExp.Body);
                }
                var pe = Expression.Parameter(typeof(EFModel.Contract));
                MethodCallExpression whereCallExpression = Expression.Call(
                                                            typeof(Queryable),
                                                            "Where",
                                                            new Type[] { queryableData.ElementType },
                                                            queryableData.Expression,
                                                            Expression.Lambda<Func<EFModel.Contract, bool>>(finalExpression, new ParameterExpression[] { pe }));

                var result= queryableData.Provider.CreateQuery(contractExp);
                result.Load();
                return null;
            }


        }

        public async Task<IEnumerable<ContractDTO>> GetCotractsAsync(QueryCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}

