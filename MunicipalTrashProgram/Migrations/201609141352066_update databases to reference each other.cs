namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabasestoreferenceeachother : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Addresses", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.UserInfoes", name: "ApplicationUser_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Workers", name: "ApplicationUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Addresses", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.UserInfoes", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Workers", name: "IX_ApplicationUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Workers", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.UserInfoes", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Addresses", name: "IX_User_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Workers", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.UserInfoes", name: "User_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Addresses", name: "User_Id", newName: "ApplicationUser_Id");
        }
    }
}
