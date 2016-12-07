using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models.SmsGateway
{
    public class SmsGateWayViewModel
    {
        public IList<EFModel.Provider> SmsGateWayList { get; set; }
        public string DefaultSmsGateWay { get; set; }
    }
}