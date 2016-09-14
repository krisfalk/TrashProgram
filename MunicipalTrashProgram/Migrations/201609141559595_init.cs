namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Address_id = c.Int(nullable: false, identity: true),
                        HouseNumber = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Address_id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Person_id = c.Int(nullable: false, identity: true),
                        Address_id = c.Int(nullable: false),
                        Worker_id = c.Int(),
                        UserInfo_id = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        _Email = c.String(),
                    })
                .PrimaryKey(t => t.Person_id)
                .ForeignKey("dbo.Addresses", t => t.Address_id, cascadeDelete: true)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_id)
                .ForeignKey("dbo.Workers", t => t.Worker_id)
                .Index(t => t.Address_id)
                .Index(t => t.Worker_id)
                .Index(t => t.UserInfo_id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfo_id = c.Int(nullable: false, identity: true),
                        PickupDay = c.String(),
                        MonthlyBill = c.Double(nullable: false),
                        YearlyBill = c.Double(nullable: false),
                        TotalBill = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserInfo_id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Worker_id = c.Int(nullable: false, identity: true),
                        WorkingZipCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Worker_id);
            
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
                        FirstName = c.String(),
                        LastName = c.String(),
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
                        address_Address_id = c.Int(),
                        userInfo_UserInfo_id = c.Int(),
                        worker_Worker_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.address_Address_id)
                .ForeignKey("dbo.UserInfoes", t => t.userInfo_UserInfo_id)
                .ForeignKey("dbo.Workers", t => t.worker_Worker_id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.address_Address_id)
                .Index(t => t.userInfo_UserInfo_id)
                .Index(t => t.worker_Worker_id);
            
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
            DropForeignKey("dbo.AspNetUsers", "worker_Worker_id", "dbo.Workers");
            DropForeignKey("dbo.AspNetUsers", "userInfo_UserInfo_id", "dbo.UserInfoes");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "address_Address_id", "dbo.Addresses");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.People", "Worker_id", "dbo.Workers");
            DropForeignKey("dbo.People", "UserInfo_id", "dbo.UserInfoes");
            DropForeignKey("dbo.People", "Address_id", "dbo.Addresses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "worker_Worker_id" });
            DropIndex("dbo.AspNetUsers", new[] { "userInfo_UserInfo_id" });
            DropIndex("dbo.AspNetUsers", new[] { "address_Address_id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.People", new[] { "UserInfo_id" });
            DropIndex("dbo.People", new[] { "Worker_id" });
            DropIndex("dbo.People", new[] { "Address_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Workers");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.People");
            DropTable("dbo.Addresses");
        }
    }
}
