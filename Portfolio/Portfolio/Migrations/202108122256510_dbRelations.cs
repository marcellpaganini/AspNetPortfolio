namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbRelations : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Authors");
            CreateTable(
                "dbo.CategoryProjects",
                c => new
                    {
                        Category_CategoryId = c.Int(nullable: false),
                        Project_ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_CategoryId, t.Project_ProjectId })
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: true)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.Project_ProjectId);
            
            AddColumn("dbo.Authors", "AuthorId", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "AuthorId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Authors", "AuthorId");
            CreateIndex("dbo.Projects", "AuthorId");
            CreateIndex("dbo.Images", "ProjectId");
            AddForeignKey("dbo.Projects", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
            AddForeignKey("dbo.Images", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            DropColumn("dbo.Authors", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Images", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CategoryProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CategoryProjects", "Category_CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Projects", "AuthorId", "dbo.Authors");
            DropIndex("dbo.CategoryProjects", new[] { "Project_ProjectId" });
            DropIndex("dbo.CategoryProjects", new[] { "Category_CategoryId" });
            DropIndex("dbo.Images", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "AuthorId" });
            DropPrimaryKey("dbo.Authors");
            DropColumn("dbo.Projects", "AuthorId");
            DropColumn("dbo.Images", "ProjectId");
            DropColumn("dbo.Authors", "AuthorId");
            DropTable("dbo.CategoryProjects");
            AddPrimaryKey("dbo.Authors", "UserId");
        }
    }
}
