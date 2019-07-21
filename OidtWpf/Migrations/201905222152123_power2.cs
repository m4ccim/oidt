namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class power2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DayStats", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DayStats", "Date", c => c.DateTime());
        }
    }
}
