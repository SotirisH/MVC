using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.SMS.ServiceTests
{
    /// <summary>
    /// Generates EF objects with the help of the Bogus.Faker
    /// </summary>
    public static class FixtureGenerator
    {
        private static Bogus.Faker faker =new Bogus.Faker();
        public static EFModel.SMSHistory CreateSMSHistory(int templateId=0)
        {
            return new EFModel.SMSHistory()
            {
                Message = faker.Lorem.Sentence(),
                MobileNumber = faker.Phone.PhoneNumber(),
                ProviderId = faker.Random.UShort(max: 10),
                ProviderFeedback = faker.Lorem.Sentence(),
                TemplateId = templateId
            };
        }

    }
}
