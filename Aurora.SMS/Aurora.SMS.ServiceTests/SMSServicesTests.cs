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
using System.Data.Common;

namespace Aurora.SMS.Service.Tests
{


    [TestClass()]
    public class SMSServicesTests
    {
        [AssemblyInitialize()]
        public static void AssemblyInit(TestContext context)
        {
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();
        }

        [TestMethod()]
        public void ConstructSMSMessagesTest()
        {
            var mockContext = new Mock<SMSDb>();


            // create an empty history
            var smsHistoryList = new List<SMSHistory>();
            var mockSMSHistorySet = EFHelper.GetQueryableMockDbSet(smsHistoryList);
            mockContext.Setup(c => c.SMSHistoryRecords).Returns(mockSMSHistorySet.Object);

            // Setup TemplateFields
            var mockTemplateFieldSet = EFHelper.GetQueryableMockDbSet(FixtureGenerator.CreateTemplateFields());
            mockContext.Setup(c => c.TemplateFields).Returns(mockTemplateFieldSet.Object);
            mockContext.Setup(m => m.Set<TemplateField>()).Returns(mockTemplateFieldSet.Object);



            // Set the template
            var template = new Template();
            template.Id = 100;
            template.Description = "MyTemplate";
            template.Name = "MyTemplate";
            template.Text = @"Dear {LastName} {FirstName},
                            your insurance with contract number:{ContractNumber} and Receipt:{ReceiptNumber} 
                            that was issued at:{IssueDate}, starts:{StartDate}, expires:{ExpireDate} at the company {CompanyDescription}
                            for the plate:{PlateNumber} has been issued. The amounts are:
                            Gross:{GrossAmount}, Tax:{TaxAmount}, net:{NetAmount}.
                            A hardcopy will be delivered at {Address} , {ZipCode}";

            var templateList = new List<Template>() { template };
            var mockTemplateSet = EFHelper.GetQueryableMockDbSet(templateList);
            // I have to use object[]
            mockTemplateSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => templateList.FirstOrDefault(d => d.Id == (int)ids[0]));

            //I need to set the Set<Template> Template  
            // because the line dbSet = DbContext.Set<T>(); on the Generic repository fails
            mockContext.Setup(m => m.Set<Template>()).Returns(mockTemplateSet.Object);
            mockContext.Setup(c => c.Templates).Returns(mockTemplateSet.Object);


            // Mocking up dbFactory
            var mockdbFactory = new Mock<DbFactory<SMSDb>>();
            mockdbFactory.Setup(m => m.Init()).Returns(mockContext.Object);
            IUnitOfWork<SMSDb> UoW = new UnitOfWork<SMSDb>(mockdbFactory.Object, "TestUser");

            var target = new SMSServices(UoW);
            var result = target.ConstructSMSMessages(FixtureGenerator.CreateSMSRecepients(), templateList.First().Id);
            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void SendBulkSMSTest()
        {
            var smsDB = Mock.Of<SMSDb>();
            var mockContext = Mock.Get(smsDB);
            // Mocking up dbFactory
            var mockdbFactory = new Mock<DbFactory<SMSDb>>();
            mockdbFactory.Setup(m => m.Init()).Returns(mockContext.Object);
            IUnitOfWork<SMSDb> UoW = new UnitOfWork<SMSDb>(mockdbFactory.Object, "TestUser");


            // create an empty history
            var smsHistoryList = new List<SMSHistory>();
            var mockSMSHistorySet = EFHelper.GetQueryableMockDbSet(smsHistoryList);
            mockContext.Setup(c => c.SMSHistoryRecords).Returns(mockSMSHistorySet.Object);
            mockContext.Setup(m => m.Set<SMSHistory>()).Returns(mockSMSHistorySet.Object);

            // Setup TemplateFields
            var mockTemplateFieldSet = EFHelper.GetQueryableMockDbSet(FixtureGenerator.CreateTemplateFields());
            mockContext.Setup(c => c.TemplateFields).Returns(mockTemplateFieldSet.Object);
            mockContext.Setup(m => m.Set<TemplateField>()).Returns(mockTemplateFieldSet.Object);



            // Set the template
            var template = new Template();
            template.Id = 100;
            template.Description = "MyTemplate";
            template.Name = "MyTemplate";
            template.Text = @"Dear {LastName} {FirstName},
                            your insurance with contract number:{ContractNumber} and Receipt:{ReceiptNumber} 
                            that was issued at:{IssueDate}, starts:{StartDate}, expires:{ExpireDate} at the company {CompanyDescription}
                            for the plate:{PlateNumber} has been issued. The amounts are:
                            Gross:{GrossAmount}, Tax:{TaxAmount}, net:{NetAmount}.
                            A hardcopy will be delivered at {Address} , {ZipCode}";

            var templateList = new List<Template>() { template };
            var mockTemplateSet = EFHelper.GetQueryableMockDbSet(templateList);
            // I have to use object[]
            mockTemplateSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => templateList.FirstOrDefault(d => d.Id == (int)ids[0]));

            //I need to set the Set<Template> Template  
            // because the line dbSet = DbContext.Set<T>(); on the Generic repository fails
            mockContext.Setup(m => m.Set<Template>()).Returns(mockTemplateSet.Object);
            mockContext.Setup(c => c.Templates).Returns(mockTemplateSet.Object);

            // Create SMS provider registration
            var provider = new Provider() { Name = "aurorafakeprovider", UserName = "Soto", PassWord = "SotoPass" };
            var providerList = new List<Provider>() { provider };
            var mockproviderSet = EFHelper.GetQueryableMockDbSet(providerList);
            mockproviderSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => providerList.FirstOrDefault(d => d.Name == (string)ids[0]));

            mockContext.Setup(m => m.Set<Provider>()).Returns(mockproviderSet.Object);
            mockContext.Setup(c => c.Providers).Returns(mockproviderSet.Object);

            var target = new SMSServices(UoW);
            var smsMessages = target.ConstructSMSMessages(FixtureGenerator.CreateSMSRecepients(100), templateList.First().Id);

            var result = target.SendBulkSMS(smsMessages, provider.Name);

            Assert.IsNotNull(result);
        }



        [TestMethod()]
        public void SendBulkSMSInMemoryTest()
        {
            http://techbrij.com/unit-testing-asp-net-mvc-controller-service

            var faker = new Bogus.Faker();

            DbConnection memoryConnection = Effort.DbConnectionFactory.CreateTransient();
            var memDB = new SMSDb(memoryConnection);

            // Mocking up dbFactory
            var mockdbFactory = new Mock<DbFactory<SMSDb>>();
            mockdbFactory.Setup(m => m.Init()).Returns(memDB);
            IUnitOfWork<SMSDb> UoW = new UnitOfWork<SMSDb>(mockdbFactory.Object, "TestUser");

            // Set the template
            var template = new Template();
            template.Id = 100;
            template.Description = "MyTemplate";
            template.Name = "MyTemplate";
            template.Text = @"Dear {LastName} {FirstName},
                            your insurance with contract number:{ContractNumber} and Receipt:{ReceiptNumber} 
                            that was issued at:{IssueDate}, starts:{StartDate}, expires:{ExpireDate} at the company {CompanyDescription}
                            for the plate:{PlateNumber} has been issued. The amounts are:
                            Gross:{GrossAmount}, Tax:{TaxAmount}, net:{NetAmount}.
                            A hardcopy will be delivered at {Address} , {ZipCode}";

            memDB.Templates.Add(template);

            // Create SMS provider registration
            var provider = new Provider() { Name = "aurorafakeprovider", UserName = "Soto", PassWord = "SotoPass",Url=faker.Internet.Url() };
            memDB.Providers.Add(provider);
            var target = new SMSServices(UoW);
            var smsMessages = target.ConstructSMSMessages(FixtureGenerator.CreateSMSRecepients(100), template.Id);

            var result = target.SendBulkSMS(smsMessages, provider.Name);

            Assert.IsNotNull(result);

        }
    }
}