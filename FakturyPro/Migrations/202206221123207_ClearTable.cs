namespace FakturyPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductDocuments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductDocuments", "Document_Id", "dbo.Documents");
            DropIndex("dbo.ProductDocuments", new[] { "Product_Id" });
            DropIndex("dbo.ProductDocuments", new[] { "Document_Id" });
            DropTable("dbo.ProductDocuments");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductDocuments",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Document_Id });
            
            CreateIndex("dbo.ProductDocuments", "Document_Id");
            CreateIndex("dbo.ProductDocuments", "Product_Id");
            AddForeignKey("dbo.ProductDocuments", "Document_Id", "dbo.Documents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ProductDocuments", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}
