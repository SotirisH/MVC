using System;
using System.Collections.Generic;
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
        
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }

        public virtual Person Person { get; set; }
    }
}
