namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcolumnstoaspnetusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserInfoes", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Workers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            CreateIndex("dbo.Addresses", "ApplicationUser_Id");
            CreateIndex("dbo.UserInfoes", "ApplicationUser_Id");
            CreateIndex("dbo.Workers", "ApplicationUser_Id");
            AddForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserInfoes", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Workers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserInfoes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Addresses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Workers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserInfoes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Addresses", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.Workers", "ApplicationUser_Id");
            DropColumn("dbo.UserInfoes", "ApplicationUser_Id");
            DropColumn("dbo.Addresses", "ApplicationUser_Id");
        }
    }
}
