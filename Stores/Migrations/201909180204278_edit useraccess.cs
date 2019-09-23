namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edituseraccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAccesses", "userID", c => c.Int(nullable: false));
            AddColumn("dbo.UserAccesses", "cateID", c => c.Int(nullable: false));
            DropColumn("dbo.UserAccesses", "Nursing");
            DropColumn("dbo.UserAccesses", "Technology");
            DropColumn("dbo.UserAccesses", "Fixing");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAccesses", "Fixing", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserAccesses", "Technology", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserAccesses", "Nursing", c => c.Boolean(nullable: false));
            DropColumn("dbo.UserAccesses", "cateID");
            DropColumn("dbo.UserAccesses", "userID");
        }
    }
}
