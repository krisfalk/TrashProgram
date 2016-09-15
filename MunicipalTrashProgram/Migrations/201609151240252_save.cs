namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class save : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Address_id", "dbo.Addresses");
            DropForeignKey("dbo.People", "UserInfo_id", "dbo.UserInfoes");
            DropForeignKey("dbo.People", "Worker_id", "dbo.Workers");
            DropIndex("dbo.People", new[] { "Address_id" });
            DropIndex("dbo.People", new[] { "Worker_id" });
            DropIndex("dbo.People", new[] { "UserInfo_id" });
            DropTable("dbo.People");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Person_id);
            
            CreateIndex("dbo.People", "UserInfo_id");
            CreateIndex("dbo.People", "Worker_id");
            CreateIndex("dbo.People", "Address_id");
            AddForeignKey("dbo.People", "Worker_id", "dbo.Workers", "Worker_id");
            AddForeignKey("dbo.People", "UserInfo_id", "dbo.UserInfoes", "UserInfo_id");
            AddForeignKey("dbo.People", "Address_id", "dbo.Addresses", "Address_id", cascadeDelete: true);
        }
    }
}
