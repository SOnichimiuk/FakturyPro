namespace FakturyPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductDocumentQuantity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductDocuments",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        DocumentId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.DocumentId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.DocumentId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDocuments", "DocumentId", "dbo.Documents");
            DropForeignKey("dbo.ProductDocuments", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductDocuments", new[] { "DocumentId" });
            DropIndex("dbo.ProductDocuments", new[] { "ProductId" });
            DropTable("dbo.ProductDocuments");
        }
    }
}
