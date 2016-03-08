namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipient", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipient", "Gender");
        }
    }
}
