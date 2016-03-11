namespace LocallySourced.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        MessageID = c.Int(nullable: false),
                        Body = c.String(),
                        Date = c.DateTime(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Member_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.AspNetUsers", t => t.Member_Id)
                .ForeignKey("dbo.Messages", t => t.MessageID)
                .Index(t => t.MessageID)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        MemberID = c.Int(nullable: false),
                        NickName = c.String(nullable: false, maxLength: 24),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Password = c.String(),
                        DateJoined = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageID = c.Int(nullable: false, identity: true),
                        MemberID = c.Int(nullable: false),
                        TopicID = c.Int(nullable: false),
                        Subject = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Member_Id = c.String(maxLength: 128),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.MessageID)
                .ForeignKey("dbo.AspNetUsers", t => t.Member_Id)
                .ForeignKey("dbo.Topics", t => t.TopicID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryID)
                .Index(t => t.TopicID)
                .Index(t => t.Member_Id)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicID = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TopicID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        ForumID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ForumID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Forum_ForumID = c.Int(),
                    })
                .PrimaryKey(t => t.CategoryID)
                .ForeignKey("dbo.Fora", t => t.Forum_ForumID)
                .Index(t => t.Forum_ForumID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Categories", "Forum_ForumID", "dbo.Fora");
            DropForeignKey("dbo.Messages", "Category_CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Comments", "MessageID", "dbo.Messages");
            DropForeignKey("dbo.Comments", "Member_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "TopicID", "dbo.Topics");
            DropForeignKey("dbo.Messages", "Member_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Categories", new[] { "Forum_ForumID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "Category_CategoryID" });
            DropIndex("dbo.Messages", new[] { "Member_Id" });
            DropIndex("dbo.Messages", new[] { "TopicID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Member_Id" });
            DropIndex("dbo.Comments", new[] { "MessageID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Fora");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Topics");
            DropTable("dbo.Messages");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
        }
    }
}
