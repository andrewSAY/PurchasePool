namespace PurchasePool.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Goods", "Description", c => c.String(maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "Description", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
