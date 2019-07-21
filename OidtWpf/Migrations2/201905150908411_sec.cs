namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StageStatDays");
        }
    }
}
