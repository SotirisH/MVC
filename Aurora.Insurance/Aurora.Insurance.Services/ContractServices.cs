using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using LinqKit;

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
        Task<IEnumerable<ContractDTO>>  GetContractsAsync(QueryCriteriaDTO criteria);
    }
    public class ContractServices : IContractServices
    {
        /* Creating dynamic queries used to be hard back to the old days. The necesity of a SQL query builder was stronger than ever 
         * leading a lot of developers to create one. Then entity framework and the LINQ expressions were introduced changing everything.
         * Although the problem reappeared: How to create a dynamic LNQ statement? The core of the dynamic LINQ is the expression trees.
         * Using them you can create anything you like and some devs created some nice implementations.
         * Personally i like both "PredicateBuilder" by Joseph Albahari, a pioneer in dynamic predicates and David's Belmont implementation using expression trees.
         * For more info:
         *  https://blogs.msdn.microsoft.com/meek/2008/05/02/linq-to-entities-combining-predicates/
         *  http://www.codeproject.com/Articles/1079028/Build-Lambda-Expressions-Dynamically
         *  https://msdn.microsoft.com/en-us/library/mt654267.aspx
         *  http://www.albahari.com/nutshell/linqkit.aspx
         */
        public IEnumerable<ContractDTO> GetContracts(QueryCriteriaDTO criteria)
        {
            
            using (var db = new InsuranceDb())
            {

                // create a dynamic query
                IQueryable<EFModel.Contract> queryableData = db.Contracts.AsExpandable();
                queryableData.Include(c => c.Person).Include(c=>c.Company).Include(c=>c.Person.Phones);

                //get only mobiles
                var finalExpression = PredicateBuilder.New<EFModel.Contract>();
                if (!string.IsNullOrWhiteSpace(criteria.ContractNumber))
                {
                    finalExpression = finalExpression.And(c=> c.ContractNumber== criteria.ContractNumber);
                 }
                if (!string.IsNullOrWhiteSpace(criteria.PlateNumber))
                {
                    finalExpression = finalExpression.And(c => c.PlateNumber == criteria.PlateNumber);
                }
                if (!string.IsNullOrWhiteSpace(criteria.CompanyId))
                {
                    finalExpression = finalExpression.And(c => c.Company.Id == criteria.CompanyId);
                }
                // TODO: Refactor date ranges
                if (criteria.ExpireDateFrom.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.ExpireDate >= criteria.ExpireDateFrom);
                }
                if (criteria.ExpireDateTo.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.ExpireDate <= criteria.ExpireDateTo);
                }

                if (criteria.IssueDateFrom.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.IssueDate >= criteria.IssueDateFrom);
                }
                if (criteria.IssueDateTo.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.IssueDate <= criteria.IssueDateTo);
                }

                if (criteria.StartDateFrom.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.StartDate >= criteria.StartDateFrom);
                }
                if (criteria.StartDateTo.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.StartDate <= criteria.StartDateTo);
                }

                if (criteria.IsCanceled.HasValue)
                {
                    finalExpression = finalExpression.And(c => c.IsCanceled == criteria.IsCanceled);
                }

                if (!string.IsNullOrWhiteSpace(criteria.FirstNameStartsWith))
                {
                    finalExpression = finalExpression.And(c => c.Person.FirstName.StartsWith(criteria.FirstNameStartsWith));
                }

                if (!string.IsNullOrWhiteSpace(criteria.LastNameStartsWith))
                {
                    finalExpression = finalExpression.And(c => c.Person.LastName.StartsWith(criteria.LastNameStartsWith));
                }

                if (!string.IsNullOrWhiteSpace(criteria.CompanyId))
                {
                    finalExpression = finalExpression.And(c => c.Company.Id == criteria.CompanyId);
                }

                // Can we use AutoMapper here?
                var t = db.Contracts.AsExpandable().Where(finalExpression).Select( s=>
                    new ContractDTO() {
                        Address=s.Person.Address,
                        BirthDate=s.Person.BirthDate,
                        CanceledDate=s.CanceledDate,
                        CompanyDescription=s.Company.Description,
                        CompanyId=s.Company.Id,
                        contractid=s.Id,
                        ContractNumber=s.ContractNumber,
                        DrivingLicenceNum=s.Person.DrivingLicenceNum,
                        ExpireDate=s.ExpireDate,
                        FatherName=s.Person.FatherName,
                        FirstName=s.Person.FirstName,
                        GrossAmount=s.GrossAmount,
                        IsCanceled=s.IsCanceled,
                        IssueDate=s.IssueDate,
                        LastName=s.Person.LastName,
                        MobileNumber=s.Person.Phones.FirstOrDefault(p=>p.PhoneType==EFModel.PhoneType.Mobile).Number,
                        NetAmount=s.NetAmount,
                        PersonId=s.PersonId,
                        PlateNumber=s.PlateNumber,
                        ReceiptNumber=s.ReceiptNumber,
                        StartDate=s.StartDate,
                        TaxAmount=s.TaxAmount,
                        TaxId=s.Person.TaxId,
                        ZipCode=s.Person.ZipCode
                        });

                return t.ToArray();
            }


        }

        public async Task<IEnumerable<ContractDTO>> GetContractsAsync(QueryCriteriaDTO criteria)
        {
            throw new NotImplementedException();
        }
    }
}

