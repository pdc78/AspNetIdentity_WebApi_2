namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyContext : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Group", "Name_Group", unique: true);
            CreateIndex("dbo.Recipient", "MobileNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Recipient", new[] { "MobileNumber" });
            DropIndex("dbo.Group", new[] { "Name_Group" });
        }
    }
}
