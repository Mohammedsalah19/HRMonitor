namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdaysoff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DaysOFFs", "reportDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DaysOFFs", "DateFrom", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DaysOFFs", "DateTo", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DaysOFFs", "DateTo", c => c.String());
            AlterColumn("dbo.DaysOFFs", "DateFrom", c => c.String());
            DropColumn("dbo.DaysOFFs", "reportDate");
        }
    }
}
