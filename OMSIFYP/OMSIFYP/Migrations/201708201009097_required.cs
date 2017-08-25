namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Person", "gender", c => c.String(nullable: false, maxLength: 7));
            AlterColumn("dbo.Person", "blood", c => c.String(nullable: false, maxLength: 5));
            AlterColumn("dbo.Person", "datebirth", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Person", "district", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Person", "nationality", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Person", "email", c => c.String(nullable: false));
            AlterColumn("dbo.Person", "Adddress", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Person", "password", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.Person", "phone");
            DropColumn("dbo.Person", "CNIC");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "CNIC", c => c.Int(nullable: false));
            AddColumn("dbo.Person", "phone", c => c.Int(nullable: false));
            AlterColumn("dbo.Person", "password", c => c.String());
            AlterColumn("dbo.Person", "Adddress", c => c.String());
            AlterColumn("dbo.Person", "email", c => c.String());
            AlterColumn("dbo.Person", "nationality", c => c.String());
            AlterColumn("dbo.Person", "district", c => c.String());
            AlterColumn("dbo.Person", "datebirth", c => c.String());
            AlterColumn("dbo.Person", "blood", c => c.String());
            AlterColumn("dbo.Person", "gender", c => c.String());
        }
    }
}
