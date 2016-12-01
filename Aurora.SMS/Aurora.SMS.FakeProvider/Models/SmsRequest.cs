using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.FakeProvider.Models
{
    public class SmsRequest
    {
        public string username { get; set; }
        public string password { get; set; }
        public string message { get; set; }
        public string mobileNumber { get; set; }
        public string messageExternalId { get; set; }
    }
}