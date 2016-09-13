namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdatabases : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.People", "Worker_id", "dbo.Workers");
            DropForeignKey("dbo.People", "UserInfo_id", "dbo.UserInfoes");
            DropForeignKey("dbo.People", "Address_id", "dbo.Addresses");
            DropIndex("dbo.People", new[] { "UserInfo_id" });
            DropIndex("dbo.People", new[] { "Worker_id" });
            DropIndex("dbo.People", new[] { "Address_id" });
            DropTable("dbo.Workers");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.People");
            DropTable("dbo.Addresses");
        }
    }
}
