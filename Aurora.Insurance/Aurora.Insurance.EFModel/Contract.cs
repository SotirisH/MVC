using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.EFModel
{
    public class Contract
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string PlateNumber { get; set; }
        public bool IsCanceled { get; set; }
        public  virtual Company Company { get; set; }
        public virtual Person Person { get; set; }


    }
}
