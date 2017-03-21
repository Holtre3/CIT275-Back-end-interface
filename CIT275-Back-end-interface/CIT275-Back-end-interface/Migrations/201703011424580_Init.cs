namespace CIT275_Back_end_interface.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                {
                    AssetID = c.Int(nullable: false, identity: true),
                    AssetType = c.Int(nullable: false),
                    IdentNbr = c.String(),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(maxLength: 500),
                    CommissionedDt = c.DateTime(nullable: false),
                    DecommissionedDt = c.DateTime(nullable: false),
                    DeploymentStatus = c.Int(nullable: false),
                    State = c.Int(nullable: false),
                    LastMaintDt = c.DateTime(nullable: false),
                    DeleteInd = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.AssetID);

            CreateTable(
                "dbo.AssetTypes",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(maxLength: 500),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.ClientAssets",
                c => new
                {
                    ClientAssetID = c.Int(nullable: false, identity: true),
                    ClientID = c.Int(nullable: false),
                    AssetID = c.Int(nullable: false),
                    EffDt = c.DateTime(nullable: false),
                    EndDt = c.DateTime(nullable: false),
                    Status = c.Int(nullable: false),
                    LastLocation = c.String(),
                })
                .PrimaryKey(t => t.ClientAssetID);

            CreateTable(
                "dbo.Clients",
                c => new
                {
                    ClientID = c.Int(nullable: false, identity: true),
                    CompanyName = c.String(nullable: false, maxLength: 200),
                    Address1 = c.String(maxLength: 200),
                    Address2 = c.String(maxLength: 200),
                    City = c.String(maxLength: 50),
                    State = c.String(maxLength: 5),
                    ZipCode = c.String(maxLength: 20),
                    Phone1 = c.String(nullable: false, maxLength: 50),
                    Phone1Type = c.Int(nullable: false),
                    Phone2 = c.String(maxLength: 50),
                    Phone2Type = c.Int(nullable: false),
                    Email = c.String(nullable: false),
                    EffDate = c.DateTime(nullable: false),
                    Active = c.Boolean(nullable: false),
                    DeleteInd = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.ClientID);

            CreateTable(
                "dbo.FileLogs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FileName = c.String(nullable: false),
                    FilePath = c.String(nullable: false),
                    ClientID = c.Int(nullable: false),
                    AssetID = c.Int(nullable: false),
                    Status = c.Int(nullable: false),
                    DeleteInd = c.Boolean(nullable: false),
                    Archived = c.Boolean(nullable: false),
                    BaseDate = c.DateTime(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.TransactionLogs",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FileID = c.Int(nullable: false),
                    MsgType = c.String(nullable: false),
                    Msg = c.String(),
                    RowID = c.Int(nullable: false),
                    EpocTime = c.Long(nullable: false),
                    CreateDate = c.DateTime(),
                    FileLog_ID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileLogs", t => t.FileLog_ID)
                .Index(t => t.FileLog_ID);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    UserType = c.Int(nullable: false),
                    ClientID = c.Int(nullable: false),
                    NewClient = c.Boolean(nullable: false),
                    CompanyName = c.String(maxLength: 200),
                    Phone = c.String(maxLength: 50),
                    ContactTime = c.String(maxLength: 50),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TransactionLogs", "FileLog_ID", "dbo.FileLogs");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.TransactionLogs", new[] { "FileLog_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TransactionLogs");
            DropTable("dbo.FileLogs");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientAssets");
            DropTable("dbo.AssetTypes");
            DropTable("dbo.Assets");
        }
    }
}
