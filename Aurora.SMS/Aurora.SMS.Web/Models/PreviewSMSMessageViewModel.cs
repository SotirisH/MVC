using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models
{
    public class PreviewSMSMessageViewModel
    {

        public IEnumerable<Aurora.SMS.Service.DTO.SMSMessageDTO> SmsMessageList { get; set; }
    }
}