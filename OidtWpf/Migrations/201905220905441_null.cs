namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _null : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "earnedGameMoney", c => c.Double());
            AlterColumn("dbo.Users", "spentGameMoney", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "spentGameMoney", c => c.Double(nullable: false));
            AlterColumn("dbo.Users", "earnedGameMoney", c => c.Double(nullable: false));
        }
    }
}
