using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.Services.DTO
{
    /// <summary>
    /// Criteria that are passed to the service
    /// </summary>
    public class QueryCriteriaDTO
    {
        public string FirstNameStartsWith { get; set; }
        public string LastNameStartsWith { get; set; }
        public string TaxId { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public DateTime? ExpireDateFrom { get; set; }
        public DateTime? ExpireDateto { get; set; }
        public string PlateNumber { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public string CompanyId { get; set; }

    }
}
