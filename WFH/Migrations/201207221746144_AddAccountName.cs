namespace WFH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accounts", "Company_ID", "dbo.Companies");
            DropIndex("dbo.Accounts", new[] { "Company_ID" });
            AddColumn("dbo.Accounts", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Accounts", "Company_ID", c => c.Int(nullable: false));
            AddForeignKey("dbo.Accounts", "Company_ID", "dbo.Companies", "ID", cascadeDelete: true);
            CreateIndex("dbo.Accounts", "Company_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Accounts", new[] { "Company_ID" });
            DropForeignKey("dbo.Accounts", "Company_ID", "dbo.Companies");
            AlterColumn("dbo.Accounts", "Company_ID", c => c.Int());
            DropColumn("dbo.Accounts", "Name");
            CreateIndex("dbo.Accounts", "Company_ID");
            AddForeignKey("dbo.Accounts", "Company_ID", "dbo.Companies", "ID");
        }
    }
}
