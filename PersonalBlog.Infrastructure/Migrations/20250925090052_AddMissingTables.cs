using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
              migrationBuilder.CreateTable(
        name: "Roles",
        columns: table => new
        {
            RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                .Annotation("Sqlite:Autoincrement", true),
            RoleName = table.Column<string>(type: "TEXT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Roles", x => x.RoleId);
        });

    migrationBuilder.CreateTable(
        name: "Tags",
        columns: table => new
        {
            TagId = table.Column<Guid>(type: "TEXT", nullable: false),
            TagName = table.Column<string>(type: "TEXT", nullable: false),
            IsPersonal = table.Column<bool>(type: "INTEGER", nullable: true),
            UserId = table.Column<Guid>(type: "TEXT", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
            UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Tags", x => x.TagId);
        });

    migrationBuilder.CreateTable(
        name: "Articles",
        columns: table => new
        {
            ArticleId = table.Column<Guid>(type: "TEXT", nullable: false),
            Title = table.Column<string>(type: "TEXT", nullable: false),
            Content = table.Column<string>(type: "TEXT", nullable: false),
            AuthorId = table.Column<Guid>(type: "TEXT", nullable: false),
            UserId = table.Column<Guid>(type: "TEXT", nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Articles", x => x.ArticleId);
        });

    migrationBuilder.CreateTable(
        name: "Comments",
        columns: table => new
        {
            CommentId = table.Column<Guid>(type: "TEXT", nullable: false),
            ArticleId = table.Column<Guid>(type: "TEXT", nullable: false),
            UserId = table.Column<Guid>(type: "TEXT", nullable: false),
            CommentText = table.Column<string>(type: "TEXT", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Comments", x => x.CommentId);
        });

    migrationBuilder.CreateTable(
        name: "ArticleTags",
        columns: table => new
        {
            ArticleId = table.Column<Guid>(type: "TEXT", nullable: false),
            TagId = table.Column<Guid>(type: "TEXT", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_ArticleTags", x => new { x.ArticleId, x.TagId });
        });

    migrationBuilder.CreateTable(
        name: "UserRoles",
        columns: table => new
        {
            UserId = table.Column<Guid>(type: "TEXT", nullable: false),
            RoleId = table.Column<int>(type: "INTEGER", nullable: false)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
        });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
