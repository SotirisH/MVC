using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Service.Providers
{
    /// <summary>
    /// The interface that all sms providers must implement
    /// in order to communicate in the same way with the Application
    /// </summary>
    public interface ISMSProvider
    {
        byte[] ProviderLogo { get; set; }
        DTO.ProviderResult SendSMS(int smsMessageId,
                                string mobileNumber,
                                string smsMessage,
                                string sender,
                                DateTime?  scheduledDate);
        string GetAvailableCredits();
    }
}
