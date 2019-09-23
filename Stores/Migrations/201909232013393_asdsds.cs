namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdsds : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "name", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Number", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Number", c => c.String());
            AlterColumn("dbo.Users", "name", c => c.String());
        }
    }
}
