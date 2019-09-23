namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addemp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        empID = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        Number = c.String(),
                        catID = c.Int(nullable: false),
                        workHour = c.String(),
                    })
                .PrimaryKey(t => t.empID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
