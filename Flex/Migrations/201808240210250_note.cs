namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class note : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainerBookings", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainerBookings", "Notes");
        }
    }
}
