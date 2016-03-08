namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recipient_2 : DbMigration
    {
        public override void Up()
        {

            CreateTable(
               "dbo.Recipient",
               c => new
               {
                   Id_Recipient = c.Guid(nullable: false, identity: true),
                   FirstName = c.String(nullable: false),
                   SecondName = c.String(nullable: false),
                   MobileNumber = c.String(nullable: false),
                   Email = c.String(nullable: false),
                   Address = c.String(),
                   City = c.String(),
                   PostalCode = c.String(),
                   Province = c.String(),
                   Country = c.String(),
                   Birthday = c.DateTime(),
                   OfficePhoneNumber = c.String(),
                   HomePhoneNumber = c.String(),
                   User_Id_Created = c.String(),
                   Date_Created = c.DateTime(),
                   User_Id_Modify = c.String(),
                   Date_Modify = c.DateTime(),
                   Active_Flg = c.Boolean(nullable: false),
               })
               .PrimaryKey(t => t.Id_Recipient);
            //DropPrimaryKey("dbo.Recipient");
            //AlterColumn("dbo.Recipient", "Id_Recipient", c => c.Guid(nullable: false, identity: true));
            //AddPrimaryKey("dbo.Recipient", "Id_Recipient");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Recipient");
            AlterColumn("dbo.Recipient", "Id_Recipient", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Recipient", "Id_Recipient");
        }
    }
}
