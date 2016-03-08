namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipRecipientGroup : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Group");
            CreateTable(
                "dbo.Recipient_Group",
                c => new
                    {
                        Id_Recipient = c.Guid(nullable: false),
                        Id_Group = c.Guid(nullable: false),
                        Date_Created = c.DateTime(),
                        User_Id_Modify = c.String(),
                        Date_Modify = c.DateTime(),
                        Active_Flg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id_Recipient, t.Id_Group })
                .ForeignKey("dbo.Group", t => t.Id_Group, cascadeDelete: true)
                .ForeignKey("dbo.Recipient", t => t.Id_Recipient, cascadeDelete: true)
                .Index(t => t.Id_Recipient)
                .Index(t => t.Id_Group);
            
            AddColumn("dbo.Group", "Id_Group", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Group", "User_Id_Created", c => c.Guid(nullable: false));
            AlterColumn("dbo.Group", "User_Id_Modify", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipient", "User_Id_Created", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipient", "User_Id_Modify", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Group", "Id_Group");
            DropColumn("dbo.Group", "Group_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Group", "Group_Id", c => c.Guid(nullable: false, identity: true));
            DropForeignKey("dbo.Recipient_Group", "Id_Recipient", "dbo.Recipient");
            DropForeignKey("dbo.Recipient_Group", "Id_Group", "dbo.Group");
            DropIndex("dbo.Recipient_Group", new[] { "Id_Group" });
            DropIndex("dbo.Recipient_Group", new[] { "Id_Recipient" });
            DropPrimaryKey("dbo.Group");
            AlterColumn("dbo.Recipient", "User_Id_Modify", c => c.String());
            AlterColumn("dbo.Recipient", "User_Id_Created", c => c.String());
            AlterColumn("dbo.Group", "User_Id_Modify", c => c.String());
            AlterColumn("dbo.Group", "User_Id_Created", c => c.String());
            DropColumn("dbo.Group", "Id_Group");
            DropTable("dbo.Recipient_Group");
            AddPrimaryKey("dbo.Group", "Group_Id");
        }
    }
}
