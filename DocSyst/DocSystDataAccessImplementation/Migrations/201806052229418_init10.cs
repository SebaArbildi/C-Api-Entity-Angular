namespace DocSystDataAccessImplementation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        EntityType = c.String(),
                        EntityId = c.Guid(),
                        ExecutingUserId = c.String(),
                        Action = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bodies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OwnStyleClass = c.String(),
                        Align = c.Int(nullable: false),
                        DocumentId = c.Guid(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Document_Id = c.Guid(),
                        Document_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id1)
                .Index(t => t.Document_Id)
                .Index(t => t.Document_Id1);
            
            CreateTable(
                "dbo.Texts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TextContent = c.String(),
                        OwnStyleClass = c.String(),
                        BodyId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bodies", t => t.BodyId)
                .Index(t => t.BodyId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        LastModifyDate = c.DateTime(nullable: false),
                        OwnStyleClass = c.String(),
                        CreatorUser_Username = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorUser_Username)
                .Index(t => t.CreatorUser_Username);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Mail = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Token = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.Formats",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StyleClasses",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        InheritedStyleClass_Id = c.Guid(),
                        Format_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StyleClasses", t => t.InheritedStyleClass_Id)
                .ForeignKey("dbo.Formats", t => t.Format_Id)
                .Index(t => t.InheritedStyleClass_Id)
                .Index(t => t.Format_Id);
            
            CreateTable(
                "dbo.Styles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Value = c.String(),
                        StyleClass_Id = c.Guid(),
                        StyleClass_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StyleClasses", t => t.StyleClass_Id)
                .ForeignKey("dbo.StyleClasses", t => t.StyleClass_Id1)
                .Index(t => t.StyleClass_Id)
                .Index(t => t.StyleClass_Id1);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StyleClasses", "Format_Id", "dbo.Formats");
            DropForeignKey("dbo.Styles", "StyleClass_Id1", "dbo.StyleClasses");
            DropForeignKey("dbo.StyleClasses", "InheritedStyleClass_Id", "dbo.StyleClasses");
            DropForeignKey("dbo.Styles", "StyleClass_Id", "dbo.StyleClasses");
            DropForeignKey("dbo.Bodies", "Document_Id1", "dbo.Documents");
            DropForeignKey("dbo.Bodies", "Document_Id", "dbo.Documents");
            DropForeignKey("dbo.Documents", "CreatorUser_Username", "dbo.Users");
            DropForeignKey("dbo.Texts", "BodyId", "dbo.Bodies");
            DropIndex("dbo.Styles", new[] { "StyleClass_Id1" });
            DropIndex("dbo.Styles", new[] { "StyleClass_Id" });
            DropIndex("dbo.StyleClasses", new[] { "Format_Id" });
            DropIndex("dbo.StyleClasses", new[] { "InheritedStyleClass_Id" });
            DropIndex("dbo.Documents", new[] { "CreatorUser_Username" });
            DropIndex("dbo.Texts", new[] { "BodyId" });
            DropIndex("dbo.Bodies", new[] { "Document_Id1" });
            DropIndex("dbo.Bodies", new[] { "Document_Id" });
            DropTable("dbo.Styles");
            DropTable("dbo.StyleClasses");
            DropTable("dbo.Formats");
            DropTable("dbo.Users");
            DropTable("dbo.Documents");
            DropTable("dbo.Texts");
            DropTable("dbo.Bodies");
            DropTable("dbo.AuditLogs");
        }
    }
}
