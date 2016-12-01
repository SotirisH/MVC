using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Providers
{
    /// <summary>
    /// The base Interaface for all client provider implementations
    /// </summary>
    public interface ISMSClientProvider
    {
        /// <summary>
        /// Main function to communicate with the provider.
        /// A text is send to the SMS gateway in order to be delivered to the specific mobileNumber
        /// </summary>
        /// <param name="smsMessageId"></param>
        /// <param name="mobileNumber"></param>
        /// <param name="smsMessage"></param>
        /// <param name="sender"></param>
        /// <param name="scheduledDate"></param>
        /// <returns>A generic result. Each concrete implemenatation of this
        /// interface is responsible to construct a proper message</returns>
        Task<SMSResult> SendSMSAsync(long smsMessageId,
                            string mobileNumber,
                            string smsMessage,
                            string sender,
                            DateTime?  scheduledDate);
        Task<string> GetAvailableCreditsAsync();
    }
}
