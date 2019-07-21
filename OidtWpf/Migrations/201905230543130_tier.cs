namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Tier", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Tier");
        }
    }
}
