using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Insurance.EFModel
{
    public enum PhoneType
    {
        Fixed,
        Mobile
    }
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
        [Required]
        public virtual Person Person { get; set; }
    }
}
