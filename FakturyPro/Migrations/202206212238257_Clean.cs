namespace FakturyPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clean : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Documents", "Client_Id", "dbo.Clients");
            DropIndex("dbo.Documents", new[] { "Client_Id" });
            DropColumn("dbo.Documents", "ClientId");
            RenameColumn(table: "dbo.Documents", name: "Client_Id", newName: "ClientId");
            DropPrimaryKey("dbo.Clients");
            AddColumn("dbo.Documents", "CreationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Clients", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Documents", "ClientId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Clients", "Id");
            CreateIndex("dbo.Documents", "ClientId");
            AddForeignKey("dbo.Documents", "ClientId", "dbo.Clients", "Id", cascadeDelete: true);
            DropColumn("dbo.Documents", "dataWystawienia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "dataWystawienia", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Documents", "ClientId", "dbo.Clients");
            DropIndex("dbo.Documents", new[] { "ClientId" });
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.Documents", "ClientId", c => c.Long());
            AlterColumn("dbo.Clients", "Id", c => c.Long(nullable: false, identity: true));
            DropColumn("dbo.Documents", "CreationDate");
            AddPrimaryKey("dbo.Clients", "Id");
            RenameColumn(table: "dbo.Documents", name: "ClientId", newName: "Client_Id");
            AddColumn("dbo.Documents", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Documents", "Client_Id");
            AddForeignKey("dbo.Documents", "Client_Id", "dbo.Clients", "Id");
        }
    }
}
