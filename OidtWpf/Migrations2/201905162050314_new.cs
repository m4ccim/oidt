namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DayStats", "StartedStages");
            DropColumn("dbo.DayStats", "FinishedStages");
            DropColumn("dbo.DayStats", "Wins");
            DropColumn("dbo.DayStats", "IncomeSum");
            DropColumn("dbo.DayStats", "IncomeSumToRevenue");
            DropColumn("dbo.DayStats", "PurchasedItems");
            DropColumn("dbo.DayStats", "PurchasedItemsIncome");
            DropColumn("dbo.DayStats", "PurchasedItemsRevenue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DayStats", "PurchasedItemsRevenue", c => c.Double(nullable: false));
            AddColumn("dbo.DayStats", "PurchasedItemsIncome", c => c.Double(nullable: false));
            AddColumn("dbo.DayStats", "PurchasedItems", c => c.Int(nullable: false));
            AddColumn("dbo.DayStats", "IncomeSumToRevenue", c => c.Double(nullable: false));
            AddColumn("dbo.DayStats", "IncomeSum", c => c.Double(nullable: false));
            AddColumn("dbo.DayStats", "Wins", c => c.Int(nullable: false));
            AddColumn("dbo.DayStats", "FinishedStages", c => c.Int(nullable: false));
            AddColumn("dbo.DayStats", "StartedStages", c => c.Int(nullable: false));
        }
    }
}
