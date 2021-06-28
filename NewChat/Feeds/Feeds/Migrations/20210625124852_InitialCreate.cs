using Microsoft.EntityFrameworkCore.Migrations;

namespace Feeds.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "newsFeeds",
                columns: table => new
                {
                    FeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_newsFeeds", x => x.FeedId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "newsFeeds");
        }
    }
}
