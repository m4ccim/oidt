namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newusers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "boughtItems", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "isCheater", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "startedStages", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "finishedStage", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "wins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "wins");
            DropColumn("dbo.Users", "finishedStage");
            DropColumn("dbo.Users", "startedStages");
            DropColumn("dbo.Users", "isCheater");
            DropColumn("dbo.Users", "boughtItems");
        }
    }
}
