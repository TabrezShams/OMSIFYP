namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "salary", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "salary");
        }
    }
}
