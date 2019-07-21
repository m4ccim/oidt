namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "earnedGameMoney", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "spentGameMoney", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "spentGameMoney");
            DropColumn("dbo.Users", "earnedGameMoney");
        }
    }
}
