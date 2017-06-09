namespace PurchasePool.Data.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryGoodReferences",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        Category_Id = c.Guid(),
                        Good_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Good_Id);
            
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                        Description = c.String(nullable: false, maxLength: 255),
                        WebLink = c.String(nullable: false, maxLength: 512),
                        DateCreate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryGoodReferences", "Good_Id", "dbo.Goods");
            DropForeignKey("dbo.CategoryGoodReferences", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CategoryGoodReferences", new[] { "Good_Id" });
            DropIndex("dbo.CategoryGoodReferences", new[] { "Category_Id" });
            DropTable("dbo.Goods");
            DropTable("dbo.CategoryGoodReferences");
            DropTable("dbo.Categories");
        }
    }
}
