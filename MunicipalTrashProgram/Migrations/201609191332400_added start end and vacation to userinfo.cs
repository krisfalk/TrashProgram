namespace MunicipalTrashProgram.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedstartendandvacationtouserinfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserInfoes", "StartDate", c => c.DateTime());
            AddColumn("dbo.UserInfoes", "EndDate", c => c.DateTime());
            AddColumn("dbo.UserInfoes", "VacationDays", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserInfoes", "VacationDays");
            DropColumn("dbo.UserInfoes", "EndDate");
            DropColumn("dbo.UserInfoes", "StartDate");
        }
    }
}
