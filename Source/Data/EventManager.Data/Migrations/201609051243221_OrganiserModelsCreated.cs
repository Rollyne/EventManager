namespace EventManager.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganiserModelsCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Creator_Id = c.String(maxLength: 128),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Creator_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.EventDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Creator_Id = c.String(maxLength: 128),
                        Event_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Creator_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Destination = c.String(),
                        Content = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Creator_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Creator_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.Creator_Id)
                .Index(t => t.ApplicationUser_Id);
            
            AddColumn("dbo.AspNetUsers", "Event_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Event_Id");
            AddForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.EventDates", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.Events", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.EventDates", "Creator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Creator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Events", new[] { "Creator_Id" });
            DropIndex("dbo.Events", new[] { "IsDeleted" });
            DropIndex("dbo.EventDates", new[] { "Event_Id" });
            DropIndex("dbo.EventDates", new[] { "Creator_Id" });
            DropIndex("dbo.EventDates", new[] { "IsDeleted" });
            DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Event_Id" });
            DropIndex("dbo.Comments", new[] { "Creator_Id" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropColumn("dbo.AspNetUsers", "Event_Id");
            DropTable("dbo.Events");
            DropTable("dbo.EventDates");
            DropTable("dbo.Comments");
        }
    }
}
