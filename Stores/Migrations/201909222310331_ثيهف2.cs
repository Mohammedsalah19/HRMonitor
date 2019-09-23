namespace Stores.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ثيهف2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DaysOFFs", "NumberOfMahdar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DaysOFFs", "NumberOfMahdar");
        }
    }
}
