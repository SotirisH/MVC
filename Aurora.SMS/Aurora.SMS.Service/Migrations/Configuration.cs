namespace Aurora.SMS.Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.SMSDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.SMSDb context)
        {

            InitTemplateField().ForEach(x => context.TemplateFields.AddOrUpdate(x));

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }


        public List<EFModel.TemplateField> InitTemplateField()
        {
            return new List<EFModel.TemplateField>
            {
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="ContractNumber",
                    Description="The number of the contract"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="ReceiptNumber",
                    Description="The number of the receipt"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="IssueDate",
                    Description="The Issue Date"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="StartDate",
                    Description="The Start Date"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="ExpireDate",
                    Description="The Expire Date"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="GrossAmount",
                    Description="The GrossAmount"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="NetAmount",
                    Description="The NetAmount"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="PlateNumber",
                    Description="The PlateNumber"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="CanceledDate",
                    Description="The CanceledDate"
                },                
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="CompanyDescription",
                    Description="The CompanyDescription"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="FirstName",
                    Description="The FirstName"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="LastName",
                    Description="The LastName"
                }
            };
        }

    }
}
