namespace CIT275_Back_end_interface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Phone2Type", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Phone2Type", c => c.Int(nullable: false));
        }
    }
}
