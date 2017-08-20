namespace Aurora.Insurance.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 7),
                        Description = c.String(nullable: false, maxLength: 50),
                        CreatedBy = c.String(maxLength: 50),
                        CreatedOn = c.DateTime(),
                        ModifiedBy = c.String(maxLength: 50),
                        ModifiedOn = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContractNumber = c.String(nullable: false, maxLength: 15),
                        ReceiptNumber = c.String(nullable: false, maxLength: 15),
                        IssueDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ExpireDate = c.DateTime(nullable: false),
                        GrossAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NetAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PlateNumber = c.String(nullable: false, maxLength: 15),
                        IsCanceled = c.Boolean(nullable: false),
                        CanceledDate = c.DateTime(),
                        PersonId = c.Int(nullable: false),
                        Company_Id = c.String(nullable: false, maxLength: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id, cascadeDelete: true)
                .ForeignKey("dbo.Person", t => t.PersonId, cascadeDelete: true)
                .Index(t => new { t.ContractNumber, t.ReceiptNumber }, unique: true, name: "IX_ContractNum_Receipt")
                .Index(t => t.PersonId)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FatherName = c.String(maxLength: 50),
                        BirthDate = c.DateTime(),
                        DrivingLicenceNum = c.String(maxLength: 50),
                        TaxId = c.String(),
                        Address = c.String(maxLength: 250),
                        ZipCode = c.String(maxLength: 12),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Phone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(nullable: false, maxLength: 50),
                        PhoneType = c.Int(nullable: false),
                        Person_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Person", t => t.Person_Id, cascadeDelete: true)
                .Index(t => t.Person_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contract", "PersonId", "dbo.Person");
            DropForeignKey("dbo.Phone", "Person_Id", "dbo.Person");
            DropForeignKey("dbo.Contract", "Company_Id", "dbo.Company");
            DropIndex("dbo.Phone", new[] { "Person_Id" });
            DropIndex("dbo.Contract", new[] { "Company_Id" });
            DropIndex("dbo.Contract", new[] { "PersonId" });
            DropIndex("dbo.Contract", "IX_ContractNum_Receipt");
            DropTable("dbo.Phone");
            DropTable("dbo.Person");
            DropTable("dbo.Contract");
            DropTable("dbo.Company");
        }
    }
}
