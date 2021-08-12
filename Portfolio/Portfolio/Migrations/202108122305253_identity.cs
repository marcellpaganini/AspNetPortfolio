namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CategoryProjects", "Category_CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddForeignKey("dbo.CategoryProjects", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryProjects", "Category_CategoryId", "dbo.Categories");
            DropPrimaryKey("dbo.Categories");
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddForeignKey("dbo.CategoryProjects", "Category_CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
