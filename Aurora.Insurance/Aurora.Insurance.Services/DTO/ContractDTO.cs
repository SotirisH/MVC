using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.Services.DTO
{
    /// <summary>
    /// The compact returned object from the service
    /// </summary>
    public class ContractDTO
    {
        public int contractid { get; set; }
        public string ContractNumber { get; set; }
        public string ReceiptNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string PlateNumber { get; set; }
        public bool IsCanceled { get; set; }
        public DateTime? CanceledDate { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string DrivingLicenceNum { get; set; }
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string CompanyId { get; set; }
        public string CompanyDescription { get; set; }
        public string MobileNumber { get; set; }
        public bool ContainsMobile {
            get {
                return !string.IsNullOrWhiteSpace(MobileNumber);
            } }

    }
}
