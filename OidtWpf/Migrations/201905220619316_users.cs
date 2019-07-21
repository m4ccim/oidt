namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        udid = c.String(),
                        parametersGender = c.String(),
                        parametersAge = c.Int(nullable: false),
                        parametersCountry = c.String(),
                        spentMoney = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
