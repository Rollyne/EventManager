namespace EventManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyEv_ApUsr : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.ApplicationUserEvents",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Event_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Event_Id);
            
            DropColumn("dbo.AspNetUsers", "Event_Id");
            DropColumn("dbo.Events", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Event_Id", c => c.Int());
            DropForeignKey("dbo.ApplicationUserEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserEvents", new[] { "Event_Id" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserEvents");
            CreateIndex("dbo.Events", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Event_Id");
            AddForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events", "Id");
        }
    }
}
