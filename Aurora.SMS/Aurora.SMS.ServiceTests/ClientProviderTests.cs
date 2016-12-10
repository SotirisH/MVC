using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurora.SMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Aurora.SMS.Service.Data;
using Aurora.SMS.EFModel;
using Aurora.SMS.ServiceTests;
using Aurora.Core.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Aurora.SMS.Service.Tests
{
    [TestClass()]
    public class ClientProviderTests
    {
        [TestMethod()]
        public void GetAvailableCredits()
        {
            var target = Providers.ClientProviderFactory.CreateClient("snailabroad", "test", "test");
            var task = target.GetAvailableCreditsAsync();
            task.Wait();
            string result = task.Result;
            Assert.IsFalse(result.StartsWith("Error!"));
        }


        [TestMethod()]
        public void SendTestSMS()
        {
            var target = Providers.ClientProviderFactory.CreateClient("snailabroad", "test", "test");
            var task = target.SendSMSAsync(200, "0982342", "Test SMS", "Soto", null);
            task.Wait();
            var result = task.Result;
            Assert.IsNotNull(result);
        }


        [TestMethod()]
        public void TestPost()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8080");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var testObj = new { echo = "echo" };

                var content = new StringContent(JsonConvert.SerializeObject(testObj), Encoding.UTF8, "application/json");

                //var content = new FormUrlEncodedContent(new[]
                //{
                //     new KeyValuePair<string, string>("echo", "echo")
                //});
                var result = client.PostAsync("/api/snailabroad/TestPost", content).Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultContent);
            }
        }
    }
}
