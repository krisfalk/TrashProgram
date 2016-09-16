namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFKworkertousers : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "worker_Worker_id", newName: "Worker_id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_worker_Worker_id", newName: "IX_Worker_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Worker_id", newName: "IX_worker_Worker_id");
            RenameColumn(table: "dbo.AspNetUsers", name: "Worker_id", newName: "worker_Worker_id");
        }
    }
}
