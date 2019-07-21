namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DayStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        DAU = c.Int(nullable: false),
                        NewUsers = c.Int(nullable: false),
                        Revenue = c.Double(nullable: false),
                        ExchangeRate = c.Double(nullable: false),
                        StartedStages = c.Int(nullable: false),
                        FinishedStages = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        IncomeSum = c.Double(nullable: false),
                        IncomeSumToRevenue = c.Double(nullable: false),
                        PurchasedItems = c.Int(nullable: false),
                        PurchasedItemsIncome = c.Double(nullable: false),
                        PurchasedItemsRevenue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        udid = c.String(),
                        date = c.DateTime(nullable: false),
                        event_id = c.Int(nullable: false),
                        ParametersId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parameters", t => t.ParametersId, cascadeDelete: true)
                .Index(t => t.ParametersId);
            
            CreateTable(
                "dbo.Parameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        gender = c.String(),
                        age = c.Int(nullable: false),
                        country = c.String(),
                        stage = c.Int(nullable: false),
                        win = c.Boolean(nullable: false),
                        time = c.Int(nullable: false),
                        income = c.Int(nullable: false),
                        name = c.String(),
                        price = c.Single(nullable: false),
                        item = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemStatDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Purchased = c.Int(nullable: false),
                        CurrencySpent = c.Double(nullable: false),
                        USD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CurrencySpent = c.Double(nullable: false),
                        USD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageStats",
                c => new
                    {
                        StageNum = c.Int(nullable: false, identity: true),
                        Started = c.Int(nullable: false),
                        Finished = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Income = c.Double(nullable: false),
                        Revenue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StageNum);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ParametersId", "dbo.Parameters");
            DropIndex("dbo.Events", new[] { "ParametersId" });
            DropTable("dbo.StageStats");
            DropTable("dbo.ItemStats");
            DropTable("dbo.ItemStatDays");
            DropTable("dbo.Parameters");
            DropTable("dbo.Events");
            DropTable("dbo.DayStats");
        }
    }
}
