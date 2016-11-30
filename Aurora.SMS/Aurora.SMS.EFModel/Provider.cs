using Aurora.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.EFModel
{
    /// <summary>
    /// A registered SMS Provider
    /// </summary>
    public class Provider:EntityBase
    {
        public string Name { get; set; }
        /// <summary>
        /// Url that points to the SMS GateWay
        /// </summary>
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool SupportsScheduleMessage { get; set; }
    }
}
