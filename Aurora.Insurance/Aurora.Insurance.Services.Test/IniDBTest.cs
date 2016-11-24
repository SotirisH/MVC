using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Aurora.Insurance.Services.Test
{
    [TestClass]
    public class IniDBTest
    {
        [TestMethod]
        public void PopulateDB()
        {
            // Init database
            System.Data.Entity.Database.SetInitializer(new SeedFakeData());
        }
    }



    public class SeedFakeData : DropCreateDatabaseIfModelChanges<InsuranceDb>
    {
        /// <summary>
        /// This class initializes the DB with fake data  usig bogus
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(InsuranceDb context)
        {
            var faker = new Bogus.Faker();
            for (var i=0; i<= 10;i++)
            {
                context.Companies.Add(new EFModel.Company()
                                        {
                                            Id= faker.s,


                }
                                                );

            }


        }
    }

}
