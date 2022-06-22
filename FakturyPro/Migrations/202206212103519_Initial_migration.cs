namespace FakturyPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Adress = c.String(),
                        City = c.String(),
                        PostalCode = c.String(),
                        NIP = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentNr = c.String(),
                        ClientId = c.Int(nullable: false),
                        dataWystawienia = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        Client_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        VatRate = c.Int(nullable: false),
                        PriceNetto = c.Double(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductDocuments",
                c => new
                    {
                        Product_Id = c.Int(nullable: false),
                        Document_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_Id, t.Document_Id })
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Documents", t => t.Document_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Document_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductDocuments", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.ProductDocuments", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Documents", "Client_Id", "dbo.Clients");
            DropIndex("dbo.ProductDocuments", new[] { "Document_Id" });
            DropIndex("dbo.ProductDocuments", new[] { "Product_Id" });
            DropIndex("dbo.Documents", new[] { "Client_Id" });
            DropTable("dbo.ProductDocuments");
            DropTable("dbo.Products");
            DropTable("dbo.Documents");
            DropTable("dbo.Clients");
        }
    }
}
