namespace EventManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImagesChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Events", "ImagesFilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ImagesFilePath", c => c.String());
        }
    }
}
