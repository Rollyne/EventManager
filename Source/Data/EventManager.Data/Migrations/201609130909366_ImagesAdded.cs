namespace EventManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PhotoFileName", c => c.String());
            AddColumn("dbo.Events", "ImagesFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImagesFilePath");
            DropColumn("dbo.AspNetUsers", "PhotoFileName");
        }
    }
}
