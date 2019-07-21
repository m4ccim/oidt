namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class two : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "udid", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "udid");
            DropColumn("dbo.Users", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "udid", c => c.String());
            AddPrimaryKey("dbo.Users", "Id");
        }
    }
}
