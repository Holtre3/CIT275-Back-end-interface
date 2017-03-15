namespace CIT275_Back_end_interface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserType", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "ClientID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "NewClient", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String(maxLength: 200));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(maxLength: 50));
            AddColumn("dbo.AspNetUsers", "ContactTime", c => c.String(maxLength: 50));
            //AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            //AddColumn("dbo.AspNetUsers", "City", c => c.String(maxLength: 50));
            //AddColumn("dbo.AspNetUsers", "State", c => c.String(maxLength: 50));
            //AddColumn("dbo.AspNetUsers", "Zip", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.AspNetUsers", "Zip");
            //DropColumn("dbo.AspNetUsers", "State");
            //DropColumn("dbo.AspNetUsers", "City");
            //DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "ContactTime");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "CompanyName");
            DropColumn("dbo.AspNetUsers", "NewClient");
            DropColumn("dbo.AspNetUsers", "ClientID");
            DropColumn("dbo.AspNetUsers", "UserType");
        }
    }
}
