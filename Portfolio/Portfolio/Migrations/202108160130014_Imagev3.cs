namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Imagev3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Images", "ContentType");
            DropColumn("dbo.Images", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Content", c => c.Binary());
            AddColumn("dbo.Images", "ContentType", c => c.String());
        }
    }
}
