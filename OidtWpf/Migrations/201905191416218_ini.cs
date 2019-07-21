namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ini : DbMigration
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
                        parametersGender = c.String(),
                        parametersAge = c.Int(nullable: false),
                        parametersCountry = c.String(),
                        parametersStage = c.Int(nullable: false),
                        parametersWin = c.Boolean(nullable: false),
                        parametersTime = c.Int(nullable: false),
                        parametersIncome = c.Int(nullable: false),
                        parametersName = c.String(),
                        parametersPrice = c.Single(nullable: false),
                        parametersItem = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ItemStatDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Date = c.DateTime(nullable: false),
                        Amount = c.Int(nullable: false),
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
                        Amount = c.Int(nullable: false),
                        CurrencySpent = c.Double(nullable: false),
                        USD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Predictions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        DAU = c.Int(nullable: false),
                        NewUsers = c.Int(nullable: false),
                        Revenue = c.Double(nullable: false),
                        SoldAmount = c.Int(nullable: false),
                        SoldUSD = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StageStatDays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        StageNum = c.Int(nullable: false),
                        Started = c.Int(nullable: false),
                        Finished = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Income = c.Double(nullable: false),
                        Revenue = c.Double(nullable: false),
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
            DropTable("dbo.StageStats");
            DropTable("dbo.StageStatDays");
            DropTable("dbo.Predictions");
            DropTable("dbo.ItemStats");
            DropTable("dbo.ItemStatDays");
            DropTable("dbo.Events");
            DropTable("dbo.DayStats");
        }
    }
}
