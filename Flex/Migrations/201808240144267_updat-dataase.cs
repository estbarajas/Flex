namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatdataase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TrainerBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.String(),
                        Time = c.String(),
                        AcceptSession = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TrainerBookings", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TrainerBookings", new[] { "UserId" });
            DropTable("dbo.TrainerBookings");
        }
    }
}
