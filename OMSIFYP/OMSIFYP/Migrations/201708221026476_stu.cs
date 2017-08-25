namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stu : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Person", "interest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "interest", c => c.String());
        }
    }
}
