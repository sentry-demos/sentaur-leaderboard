using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sentaur.Leaderboard.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "ScoreEntries",
                type: "text",
                nullable: true);

            migrationBuilder.Sql("""
                UPDATE "ScoreEntries"
                SET "Platform" = CASE (floor(random() * 3)::int)
                    WHEN 0 THEN 'Xbox'
                    WHEN 1 THEN 'PlayStation'
                    ELSE 'Switch'
                END
                WHERE "Platform" IS NULL;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "ScoreEntries");
        }
    }
}
