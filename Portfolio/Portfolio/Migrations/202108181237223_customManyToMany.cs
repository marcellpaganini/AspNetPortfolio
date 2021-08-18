namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customManyToMany : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Authors");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.Projects");
            CreateTable(
                "dbo.CategoryProjects",
                c => new
                    {
                        CategoryId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoryId, t.ProjectId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Authors", "AuthorId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Images", "FilePath", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Images", "UploadDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Images", "ProjectId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "AuthorId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Images", "ImageId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Projects", "ProjectId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Authors", "AuthorId");
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddPrimaryKey("dbo.Images", "ImageId");
            AddPrimaryKey("dbo.Projects", "ProjectId");
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
            DropForeignKey("dbo.CategoryProjects", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CategoryProjects", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Projects", "AuthorId", "dbo.Authors");
            DropIndex("dbo.Images", new[] { "ProjectId" });
            DropIndex("dbo.CategoryProjects", new[] { "ProjectId" });
            DropIndex("dbo.CategoryProjects", new[] { "CategoryId" });
            DropIndex("dbo.Projects", new[] { "AuthorId" });
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.Categories");
            DropPrimaryKey("dbo.Authors");
            AlterColumn("dbo.Projects", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Images", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "AuthorId");
            DropColumn("dbo.Images", "ProjectId");
            DropColumn("dbo.Images", "UploadDateTime");
            DropColumn("dbo.Images", "FilePath");
            DropColumn("dbo.Authors", "AuthorId");
            DropTable("dbo.CategoryProjects");
            AddPrimaryKey("dbo.Projects", "ProjectId");
            AddPrimaryKey("dbo.Images", "ImageId");
            AddPrimaryKey("dbo.Categories", "CategoryId");
            AddPrimaryKey("dbo.Authors", "UserId");
        }
    }
}
