namespace CIT275_Back_end_interface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class client : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Country", c => c.String(maxLength: 50));
            AlterColumn("dbo.Clients", "State", c => c.String(maxLength: 50));
            AlterColumn("dbo.Clients", "Phone1Type", c => c.Int());
            AlterColumn("dbo.Clients", "Email", c => c.String());
            AlterColumn("dbo.Clients", "Active", c => c.Boolean());
            AlterColumn("dbo.Clients", "DeleteInd", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "DeleteInd", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Clients", "Active", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Clients", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Clients", "Phone1Type", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "State", c => c.String(maxLength: 5));
            DropColumn("dbo.Clients", "Country");
        }
    }
}
