using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.EFModel
{
    public enum MessageStatus
    {
        Pending,
        Delivered,
        Error
    }
    public class SMSHistory
    {
        public int id { get; set; }
        /// <summary>
        /// Ths SMS message that has been sent to the SMs server
        /// </summary>
        public string Message { get; set; }
        public MessageStatus Status { get; set; }
        public DateTime SendDateTime { get; set; }
        public string MobileNumber { get; set; }
        public int? PersonId { get; set; }
        public int? ContractId { get; set; }
        
        
        /// <summary>
        /// The identity of the message that has been assinged by the SMS provider
        /// </summary>
        public string ProviderMsgId { get; set; }
        public string ProviderFeedback { get; set; }
        public DateTime ProviderFeedBackDateTime { get; set; }

        public  int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

    }
}
