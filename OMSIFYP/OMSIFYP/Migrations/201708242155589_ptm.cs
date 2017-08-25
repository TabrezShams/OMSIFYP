namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ptm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PTMCalls", "Creater", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PTMCalls", "Creater");
        }
    }
}
