namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Employees", "Number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "Number", c => c.String());
            AlterColumn("dbo.Employees", "name", c => c.String());
        }
    }
}
