namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userstablefkadded : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Address_Address_id", newName: "Address_id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Address_Address_id", newName: "IX_Address_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Address_id", newName: "IX_Address_Address_id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Address_id", newName: "Address_Address_id");
        }
    }
}
