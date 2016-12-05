using Aurora.Core.Data;
using Aurora.SMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.EFModel
{

    public class SMSHistory:EntityBase
    {
        /// <summary>
        /// The id is long because we expect huge number of messages to be sent over the years
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Everytime the user sends a batch of messages a session is created
        /// </summary>
        public Guid SessionId { get; set; }
        public string SessionName { get; set; }
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
        /// The identity that has been given by the SMS provider to the SMS 
        /// </summary>
        public string ProviderMsgId { get; set; }
        public string ProviderFeedback { get; set; }
        public DateTime? ProviderFeedBackDateTime { get; set; }
        public  string ProviderName { get; set; }
        public Provider Provider { get; set; }

        public int TemplateId { get; set; }
        public Template Template { get; set; }

    }
}
