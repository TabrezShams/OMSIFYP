namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class superadmin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SuperAdminCre",
                c => new
                    {
                        SuperAdminCreID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        pass = c.String(nullable: false),
                        email = c.String(nullable: false),
                        imgUrl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SuperAdminCreID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SuperAdminCre");
        }
    }
}
