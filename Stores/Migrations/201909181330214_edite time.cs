namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DaysOFFs", "DateFrom", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.DaysOFFs", "DateTo", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DaysOFFs", "DateTo", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DaysOFFs", "DateFrom", c => c.DateTime(nullable: false));
        }
    }
}
