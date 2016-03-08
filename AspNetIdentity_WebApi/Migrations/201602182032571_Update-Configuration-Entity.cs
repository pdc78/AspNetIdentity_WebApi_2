namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateConfigurationEntity : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Group", "Name_Group", c => c.String(maxLength: 150));
            AlterColumn("dbo.Group", "Description_Group", c => c.String(maxLength: 150));
            AlterColumn("dbo.Recipient", "FirstName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Recipient", "SecondName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Recipient", "MobileNumber", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Recipient", "Email", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Recipient", "Address", c => c.String(maxLength: 300));
            AlterColumn("dbo.Recipient", "City", c => c.String(maxLength: 150));
            AlterColumn("dbo.Recipient", "PostalCode", c => c.String(maxLength: 15));
            AlterColumn("dbo.Recipient", "Province", c => c.String(maxLength: 150));
            AlterColumn("dbo.Recipient", "Country", c => c.String(maxLength: 150));
            AlterColumn("dbo.Recipient", "OfficePhoneNumber", c => c.String(maxLength: 15));
            AlterColumn("dbo.Recipient", "HomePhoneNumber", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipient", "HomePhoneNumber", c => c.String());
            AlterColumn("dbo.Recipient", "OfficePhoneNumber", c => c.String());
            AlterColumn("dbo.Recipient", "Country", c => c.String());
            AlterColumn("dbo.Recipient", "Province", c => c.String());
            AlterColumn("dbo.Recipient", "PostalCode", c => c.String());
            AlterColumn("dbo.Recipient", "City", c => c.String());
            AlterColumn("dbo.Recipient", "Address", c => c.String());
            AlterColumn("dbo.Recipient", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Recipient", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Recipient", "SecondName", c => c.String(nullable: false));
            AlterColumn("dbo.Recipient", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Group", "Description_Group", c => c.String());
            AlterColumn("dbo.Group", "Name_Group", c => c.String());
        }
    }
}
