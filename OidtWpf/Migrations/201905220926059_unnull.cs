namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "parametersAge", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "spentMoney", c => c.Single(nullable: false));
            AlterColumn("dbo.Users", "earnedGameMoney", c => c.Double(nullable: false));
            AlterColumn("dbo.Users", "spentGameMoney", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "spentGameMoney", c => c.Double());
            AlterColumn("dbo.Users", "earnedGameMoney", c => c.Double());
            AlterColumn("dbo.Users", "spentMoney", c => c.Single());
            AlterColumn("dbo.Users", "parametersAge", c => c.Int());
        }
    }
}
