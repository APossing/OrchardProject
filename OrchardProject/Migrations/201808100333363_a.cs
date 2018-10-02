namespace OrchardProject.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InventoryItem", "QuantitySMWConventional", c => c.Double(nullable: false));
            AddColumn("dbo.InventoryItem", "QuantitySMWOrganic", c => c.Double(nullable: false));
            AddColumn("dbo.InventoryItem", "QuantityKTWConventional", c => c.Double(nullable: false));
            AddColumn("dbo.InventoryItem", "QuantityKTWOrganic", c => c.Double(nullable: false));
            DropColumn("dbo.InventoryItem", "QuantitySMW");
            DropColumn("dbo.InventoryItem", "QuantityKTW");
        }

        public override void Down()
        {
            AddColumn("dbo.InventoryItem", "QuantityKTW", c => c.Double(nullable: false));
            AddColumn("dbo.InventoryItem", "QuantitySMW", c => c.Double(nullable: false));
            DropColumn("dbo.InventoryItem", "QuantityKTWOrganic");
            DropColumn("dbo.InventoryItem", "QuantityKTWConventional");
            DropColumn("dbo.InventoryItem", "QuantitySMWOrganic");
            DropColumn("dbo.InventoryItem", "QuantitySMWConventional");
        }
    }
}