namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventfkbeingadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Events", "UserId");
            AddForeignKey("dbo.Events", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "UserId" });
            DropColumn("dbo.Events", "UserId");
        }
    }
}
