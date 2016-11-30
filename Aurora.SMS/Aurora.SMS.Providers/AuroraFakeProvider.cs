using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.Providers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Example:https://www.asp.net/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    /// </remarks>
     class AuroraFakeProvider : ISMSClientProvider
    {
        private static HttpClient client = new HttpClient();

        private readonly string _userName,_password;

        public AuroraFakeProvider(string userName,
                                    string password)
        {
            _userName = userName;
            _password = password;
            client.BaseAddress = new Uri("http://localhost:8080/api/SMSGateway");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> GetAvailableCreditsAsync()
        {
            string path = @"http://localhost:8080/api/SMSGateway/GetAvailableCredits?username=" + _userName + "&password=" + _password;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            {
                return "Error!" + response.ReasonPhrase;
            }
        }

        public async Task<SMSResult> SendSMSAsync(int smsMessageId, string mobileNumber, string smsMessage, string sender, DateTime? scheduledDate)
        {
            throw new NotImplementedException();
        }
    }
}
