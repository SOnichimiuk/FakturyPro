namespace FakturyPro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class typeindocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Type");
        }
    }
}
