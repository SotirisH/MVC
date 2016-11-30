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


namespace Aurora.SMS.Service.Tests
{
    [TestClass()]
    public class ClientProviderTests
    {
        [TestMethod()]
        public void GetAvailableCredits()
        {
            var target = Providers.ClientProviderFactory.CreateClient("AuroraFakeProvider","test","test");
            var task= target.GetAvailableCredits();
            task.Wait();
            string result = task.Result;
            Assert.IsFalse(result.StartsWith("Error!"));
        }
    }
}
