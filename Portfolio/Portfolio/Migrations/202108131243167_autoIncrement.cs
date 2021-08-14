namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoIncrement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Projects", "AuthorId", "dbo.Authors");
            DropForeignKey("dbo.CategoryProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Images", "ProjectId", "dbo.Projects");
            DropPrimaryKey("dbo.Authors");
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Authors", "AuthorId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Projects", "ProjectId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Images", "ImageId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Authors", "AuthorId");
            AddPrimaryKey("dbo.Projects", "ProjectId");
            AddPrimaryKey("dbo.Images", "ImageId");
            AddForeignKey("dbo.Projects", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProjects", "Project_ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.Images", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CategoryProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "AuthorId", "dbo.Authors");
            DropPrimaryKey("dbo.Images");
            DropPrimaryKey("dbo.Projects");
            DropPrimaryKey("dbo.Authors");
            AlterColumn("dbo.Images", "ImageId", c => c.Int(nullable: false));
            AlterColumn("dbo.Projects", "ProjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.Authors", "AuthorId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Images", "ImageId");
            AddPrimaryKey("dbo.Projects", "ProjectId");
            AddPrimaryKey("dbo.Authors", "AuthorId");
            AddForeignKey("dbo.Images", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.CategoryProjects", "Project_ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            AddForeignKey("dbo.Projects", "AuthorId", "dbo.Authors", "AuthorId", cascadeDelete: true);
        }
    }
}
