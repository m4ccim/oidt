namespace OidtWpf.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class guid : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "udid", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Users", "udid");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "udid", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "udid");
        }
    }
}
