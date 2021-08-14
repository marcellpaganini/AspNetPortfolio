namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imageUpload : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "ContentType", c => c.String());
            AddColumn("dbo.Images", "Content", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Content");
            DropColumn("dbo.Images", "ContentType");
        }
    }
}
