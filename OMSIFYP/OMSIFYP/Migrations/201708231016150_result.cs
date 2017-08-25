namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class result : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnrollStudent", "sessional1", c => c.Int(nullable: false));
            AddColumn("dbo.EnrollStudent", "sessional2", c => c.Int(nullable: false));
            AddColumn("dbo.EnrollStudent", "sessional3", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnrollStudent", "sessional3");
            DropColumn("dbo.EnrollStudent", "sessional2");
            DropColumn("dbo.EnrollStudent", "sessional1");
        }
    }
}
