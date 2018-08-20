namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lmaokai : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "DayOfWeek", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "DayOfWeek");
        }
    }
}
