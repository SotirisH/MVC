using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.EFModel
{
    public class Person
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]    
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FatherName { get; set; }
        public DateTime? BirthDate { get; set; }
        [StringLength(50)]
        public string DrivingLicenceNum { get; set; }
        public string TaxId { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(7)]
        public string ZipCode { get; set; }
    }
}
