using Aurora.Insurance.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models
{
    public class CustomerSelectionViewModel
    {
        public QueryCriteriaDTO Criteria { get; set; }
        public IEnumerable<Insurance.EFModel.Company> Companies { get; set; }
        public IEnumerable<Aurora.Insurance.Services.DTO.ContractDTO> Contracts { get; set; }
    }
}