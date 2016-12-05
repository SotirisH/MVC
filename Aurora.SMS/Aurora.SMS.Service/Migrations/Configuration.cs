namespace Aurora.SMS.Service.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aurora.SMS.Service.Data.SMSDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Aurora.SMS.Service.Data.SMSDb context)
        {
           
            InitTemplateField().ForEach(x => context.TemplateFields.AddOrUpdate(x));
            context.SaveChanges();
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
                    Description="The Issue Date",
                    DataFormat="dd/MM/yyyy"

                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="StartDate",
                    Description="The Start Date",
                    DataFormat="dd/MM/yyyy"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="ExpireDate",
                    Description="The Expire Date",
                    DataFormat="dd/MM/yyyy"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="GrossAmount",
                    Description="The GrossAmount",
                    DataFormat="0.00"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="NetAmount",
                    Description="The NetAmount",
                    DataFormat="0.00"
                },
                new EFModel.TemplateField
                {
                    GroupName="Contract",
                    Name="TaxAmount",
                    Description="The TaxAmount",
                    DataFormat="0.00"
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
                    Description="The CanceledDate",
                    DataFormat="dd/MM/yyyy"
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
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="DrivingLicenceNum",
                    Description="The LastName"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="TaxId",
                    Description="The LastName"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="Address",
                    Description="The LastName"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="ZipCode",
                    Description="The LastName"
                },
                new EFModel.TemplateField
                {
                    GroupName="Person",
                    Name="BirthDate",
                    Description="The LastName",
                    DataFormat="dd/MM/yyyy"
                }
            };
        }
    }
}
