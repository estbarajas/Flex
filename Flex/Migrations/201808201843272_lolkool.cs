namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lolkool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerClasses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ClassId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerClasses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerClasses", "ClassId", "dbo.Classes");
            DropIndex("dbo.CustomerClasses", new[] { "UserId" });
            DropIndex("dbo.CustomerClasses", new[] { "ClassId" });
            DropTable("dbo.CustomerClasses");
        }
    }
}
