namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipRecipientGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipient_Group", "User_Id_Created", c => c.Guid(nullable: false));
            AlterColumn("dbo.Group", "Date_Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Group", "User_Id_Modify", c => c.Guid());
            AlterColumn("dbo.Recipient_Group", "Date_Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Recipient_Group", "User_Id_Modify", c => c.Guid());
            AlterColumn("dbo.Recipient", "Date_Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Recipient", "User_Id_Modify", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipient", "User_Id_Modify", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipient", "Date_Created", c => c.DateTime());
            AlterColumn("dbo.Recipient_Group", "User_Id_Modify", c => c.String());
            AlterColumn("dbo.Recipient_Group", "Date_Created", c => c.DateTime());
            AlterColumn("dbo.Group", "User_Id_Modify", c => c.Guid(nullable: false));
            AlterColumn("dbo.Group", "Date_Created", c => c.DateTime());
            DropColumn("dbo.Recipient_Group", "User_Id_Created");
        }
    }
}
