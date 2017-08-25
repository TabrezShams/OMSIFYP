namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentupdate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "fathercnic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "fathercnic");
        }
    }
}
