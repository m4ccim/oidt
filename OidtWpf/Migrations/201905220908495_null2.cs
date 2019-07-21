namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class null2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "parametersAge", c => c.Int());
            AlterColumn("dbo.Users", "spentMoney", c => c.Single());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "spentMoney", c => c.Single(nullable: false));
            AlterColumn("dbo.Users", "parametersAge", c => c.Int(nullable: false));
        }
    }
}
