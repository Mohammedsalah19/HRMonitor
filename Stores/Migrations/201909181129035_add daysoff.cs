namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddaysoff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DaysOFFs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        empID = c.Int(nullable: false),
                        Number = c.String(),
                        DateFrom = c.String(),
                        DateTo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.DaysOFFs");
        }
    }
}
