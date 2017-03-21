namespace CIT275_Back_end_interface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phonetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Phone1Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "Phone2Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Phone2Type", c => c.Int());
            AlterColumn("dbo.Clients", "Phone1Type", c => c.Int());
        }
    }
}
