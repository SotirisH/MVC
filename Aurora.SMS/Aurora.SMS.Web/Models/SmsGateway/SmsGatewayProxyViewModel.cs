using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aurora.SMS.Web.Models.SmsGateway
{
    /// <summary>
    /// View model for Change.cshtml
    /// The ViewModel encapsulated the values of the BootStrap Classes
    /// </summary>
    public class SmsGatewayProxyViewModel
    {
        public string LogoUrl { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Pasword { get; set; }
        public bool IsDefault { get; set; }

        /// <summary>
        /// Returns the value of the class name if this proxy is the default.
        /// Possible values are "Active" or string.Empty
        /// </summary>
        public string NavPillActiveClassValue
        {
            get
            {
                return IsDefault ? "active" : string.Empty;
            }
        }

        public string TabPageActiveClassValue
        {
            get
            {
                return IsDefault ? "active in" : string.Empty;
            }
        }

    }
}