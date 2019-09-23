namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editemp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "CategoryName", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "catID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Employees", "catID", c => c.Int(nullable: false));
            DropColumn("dbo.Employees", "CategoryName");
        }
    }
}
