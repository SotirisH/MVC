namespace Aurora.SMS.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Provider",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 50),
                    Url = c.String(nullable: false, maxLength: 255),
                    LogoUrl = c.String(),
                    UserName = c.String(maxLength: 50),
                    PassWord = c.String(maxLength: 50),
                    SupportsScheduleMessage = c.Boolean(nullable: false),
                    CreatedBy = c.String(maxLength: 50),
                    CreatedOn = c.DateTime(),
                    ModifiedBy = c.String(maxLength: 50),
                    ModifiedOn = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Name);

            CreateTable(
                "dbo.SMSHistory",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    SessionId = c.Guid(nullable: false),
                    SessionName = c.String(nullable: false, maxLength: 255),
                    Message = c.String(nullable: false),
                    Status = c.Int(nullable: false),
                    SendDateTime = c.DateTime(nullable: false),
                    MobileNumber = c.String(maxLength: 50),
                    PersonId = c.Int(),
                    ContractId = c.Int(),
                    ProviderMsgId = c.String(maxLength: 255),
                    ProviderFeedback = c.String(),
                    ProviderFeedBackDateTime = c.DateTime(),
                    ProviderName = c.String(nullable: false, maxLength: 50),
                    TemplateId = c.Int(nullable: false),
                    CreatedBy = c.String(maxLength: 50),
                    CreatedOn = c.DateTime(),
                    ModifiedBy = c.String(maxLength: 50),
                    ModifiedOn = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderName, cascadeDelete: true)
                .ForeignKey("dbo.Template", t => t.TemplateId, cascadeDelete: true)
                .Index(t => t.ProviderName)
                .Index(t => t.TemplateId);

            CreateTable(
                "dbo.Template",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(maxLength: 255),
                    Text = c.String(nullable: false),
                    IsInactive = c.Boolean(nullable: false),
                    CreatedBy = c.String(maxLength: 50),
                    CreatedOn = c.DateTime(),
                    ModifiedBy = c.String(maxLength: 50),
                    ModifiedOn = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "dbo.TemplateField",
                c => new
                {
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(maxLength: 255),
                    GroupName = c.String(nullable: false, maxLength: 50),
                    DataFormat = c.String(maxLength: 50),
                    CreatedBy = c.String(maxLength: 50),
                    CreatedOn = c.DateTime(),
                    ModifiedBy = c.String(maxLength: 50),
                    ModifiedOn = c.DateTime(),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.Name);

            // FROM FILE
            var sqlFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Migrations\Init.sql");
            SqlFile(sqlFilePath);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SMSHistory", "TemplateId", "dbo.Template");
            DropForeignKey("dbo.SMSHistory", "ProviderName", "dbo.Provider");
            DropIndex("dbo.Template", new[] { "Name" });
            DropIndex("dbo.SMSHistory", new[] { "TemplateId" });
            DropIndex("dbo.SMSHistory", new[] { "ProviderName" });
            DropTable("dbo.TemplateField");
            DropTable("dbo.Template");
            DropTable("dbo.SMSHistory");
            DropTable("dbo.Provider");
        }
    }
}
