using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service.DTO
{
     public class SMSMessageDTO
    {
        /// <summary>
        /// The msg that is constructed
        /// based on a templated and will be sent to the provider
        /// </summary>
        public string Message { get; set; }
        public string MobileNumber { get; set; }
        public int? PersonId { get; set; }
        public int? ContractId { get; set; }
        public int TemplateId { get; set; }
    }
}
