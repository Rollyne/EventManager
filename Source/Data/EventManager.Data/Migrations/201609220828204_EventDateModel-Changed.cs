namespace EventManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventDateModelChanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventDates", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.EventDates", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "StartEventDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Events", "EndEventDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.EventDates", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EventDates", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Events", "EndEventDate");
            DropColumn("dbo.Events", "StartEventDate");
            DropColumn("dbo.EventDates", "EndDate");
            DropColumn("dbo.EventDates", "StartDate");
        }
    }
}
