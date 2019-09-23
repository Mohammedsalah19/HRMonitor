namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addasadmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AsAdmin");
        }
    }
}
