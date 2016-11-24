using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.EFModel
{
    /// <summary>
    /// Represents an insurance contract for a vehicle
    /// </summary>
    public class Contract
    {
        public int Id { get; set; }
        [Index("IX_ContractNum_Receipt", 1,IsUnique =true)]

        [Required]
        [StringLength(15)]
        public string ContractNumber { get; set; }
        [Index("IX_ContractNum_Receipt", 2, IsUnique = true)]
        [Required]
        [StringLength(15)]
        public string ReceiptNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        [Required]
        public decimal GrossAmount { get; set; }
        [Required]
        public decimal NetAmount { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        [Required]
        [StringLength(15)]
        public string PlateNumber { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public  virtual Company Company { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
