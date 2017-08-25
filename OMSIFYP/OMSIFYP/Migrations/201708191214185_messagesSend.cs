namespace OMSIFYP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class messagesSend : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MessageSend",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        email = c.String(),
                        Sender = c.String(),
                        subject = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MessageSend");
        }
    }
}
