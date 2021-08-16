namespace Portfolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreImageFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "FilePath", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Images", "UploadDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "UploadDateTime");
            DropColumn("dbo.Images", "FilePath");
        }
    }
}
