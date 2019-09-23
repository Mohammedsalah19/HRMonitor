namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editondayof : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DaysOFFs", "DayofDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DaysOFFs", "DayofDate");
        }
    }
}
