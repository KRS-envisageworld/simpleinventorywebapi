namespace InventoryManagement.AppContext
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Moniker = c.String(nullable: false, maxLength: 8),
                        Name = c.String(nullable: false, maxLength: 100),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Moniker = c.String(nullable: false, maxLength: 8),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Price = c.Double(nullable: false),
                        AvailableQuantity = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InventoryItems", "Category_Id", "dbo.Categories");
            DropIndex("dbo.InventoryItems", new[] { "Category_Id" });
            DropTable("dbo.InventoryItems");
            DropTable("dbo.Categories");
        }
    }
}
