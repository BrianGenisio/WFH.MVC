namespace WFH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDayAtHomeAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DayAtHomes", "Account_UserID", c => c.Guid());
            AddForeignKey("dbo.DayAtHomes", "Account_UserID", "dbo.Accounts", "UserID");
            CreateIndex("dbo.DayAtHomes", "Account_UserID");
            DropColumn("dbo.DayAtHomes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayAtHomes", "Name", c => c.String());
            DropIndex("dbo.DayAtHomes", new[] { "Account_UserID" });
            DropForeignKey("dbo.DayAtHomes", "Account_UserID", "dbo.Accounts");
            DropColumn("dbo.DayAtHomes", "Account_UserID");
        }
    }
}
