using Aurora.SMS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service.DTO
{
    public class SmsHistoryCriteriaDTO
    {
        public Guid? SessionId { get; set; }
        /// <summary>
        /// Displays messages from history for every status in the list
        /// </summary>
        public HashSet<MessageStatus> MessageStatusList { get; set; }
        public DateTime? SendDateFrom { get; set; }
        public DateTime? SendDateTo { get; set; }
        public MessageStatus MessageStatus { get; set; }
    }
}
