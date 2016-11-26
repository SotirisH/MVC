using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurora.Insurance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Insurance.Services.DTO;

namespace Aurora.Insurance.Services.Tests
{
    [TestClass()]
    public class ContractServicesTests
    {
        [TestMethod()]
        public void GetContractsTest()
        {
            var target = new ContractServices();
            QueryCriteriaDTO queryCriteriaDTO = new QueryCriteriaDTO();
            queryCriteriaDTO.ContractNumber = "5822438";

            var result=target.GetContracts(queryCriteriaDTO);
            Assert.IsNotNull(result);
        }
    }
}