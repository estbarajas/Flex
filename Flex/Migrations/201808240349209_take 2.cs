namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class take2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                        MuscleGroup = c.String(),
                        EquipmentNeeded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Exercises");
        }
    }
}
