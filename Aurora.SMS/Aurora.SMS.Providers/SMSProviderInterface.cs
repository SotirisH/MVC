using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Providers
{
    /// <summary>
    /// The interface that all sms providers must implement
    /// in order to communicate in the same way with the Application
    /// </summary>
    public interface SMSProviderInterface
    {
        byte[] ProviderLogo { get; set; }
        in As AhsSmsInfo.SmsProviderInfo
         SendSMS(mobileNumber As String,
                    smsMessage As String,
                    sender As String,
                    scheduledDate As DateTime?) As AhsSmsInfo.SmsProviderSendResult



        string GetAvailableCredits();

    Function GetReport(undeliveredMessages As AhsSmsInfo.Lists.SMSMessageInfoList) As List(Of AhsSmsInfo.SmsProviderReportItem)
    }
}
