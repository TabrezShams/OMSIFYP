namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "oldschoolname", c => c.String());
            AddColumn("dbo.Person", "oldclass", c => c.Int());
            AddColumn("dbo.Person", "schoolcertificate", c => c.String());
            AddColumn("dbo.Person", "oldschaddress", c => c.String());
            AddColumn("dbo.Person", "oldschcity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Person", "oldschcity");
            DropColumn("dbo.Person", "oldschaddress");
            DropColumn("dbo.Person", "schoolcertificate");
            DropColumn("dbo.Person", "oldclass");
            DropColumn("dbo.Person", "oldschoolname");
        }
    }
}
