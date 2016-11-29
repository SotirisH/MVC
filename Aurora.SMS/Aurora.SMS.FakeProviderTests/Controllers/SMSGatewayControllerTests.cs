using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurora.SMS.FakeProvider.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http;

namespace Aurora.SMS.FakeProvider.Controllers.Tests
{
    [TestClass()]
    public class SMSGatewayControllerTests
    {
        [TestMethod()]
        public void SendSMSTest()
        {
            var target = Moq.Mock.Of<SMSGatewayController>();
            
            var moqController = Moq.Mock.Get(target);
            moqController.Setup(c => c.DecreaseCredit()).Verifiable();
            // unit tests shoul never bee delayed
            moqController.Setup(c => c.ApplyDelay()).Verifiable();

           
            var results = new List<Models.Result>();
            // send 100 mesagges
            for (var i = 0; i < 100; i++)
            {
                var faker = new Bogus.Faker();
                var httpResult = target.SendSMS(faker.Internet.UserName(), "Pass", faker.Lorem.Sentence(), faker.Phone.PhoneNumber(), faker.Random.UInt().ToString());
                
                results.Add(httpResult);
            }

            Assert.AreEqual(100, results.Count);


        }
    }
}