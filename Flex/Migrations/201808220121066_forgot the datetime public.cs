namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forgotthedatetimepublic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerProgresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProgressDate = c.DateTime(nullable: false),
                        CurrentWeight = c.String(),
                        WeightGoal = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerProgresses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CustomerProgresses", new[] { "UserId" });
            DropTable("dbo.CustomerProgresses");
        }
    }
}
