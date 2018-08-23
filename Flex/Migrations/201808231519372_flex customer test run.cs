namespace Flex.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flexcustomertestrun : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Customers", newName: "FlexCustomers");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FlexCustomers", newName: "Customers");
        }
    }
}
