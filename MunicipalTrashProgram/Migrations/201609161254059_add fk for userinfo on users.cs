namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfkforuserinfoonusers : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "userInfo_UserInfo_id", newName: "UserInfo_id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_userInfo_UserInfo_id", newName: "IX_UserInfo_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_UserInfo_id", newName: "IX_userInfo_UserInfo_id");
            RenameColumn(table: "dbo.AspNetUsers", name: "UserInfo_id", newName: "userInfo_UserInfo_id");
        }
    }
}
