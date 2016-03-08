namespace AspNetIdentity_WebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Group_Id = c.Guid(nullable: false, identity: true),
                        Name_Group = c.String(),
                        Description_Group = c.String(),
                        User_Id_Created = c.String(),
                        Date_Created = c.DateTime(),
                        User_Id_Modify = c.String(),
                        Date_Modify = c.DateTime(),
                        Active_Flg = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Group");
        }
    }
}
