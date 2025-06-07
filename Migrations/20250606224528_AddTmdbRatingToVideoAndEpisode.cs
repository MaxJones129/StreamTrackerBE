using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddTmdbRatingToVideoAndEpisode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TmdbRating",
                table: "Videos",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TmdbRating",
                table: "Episodes",
                type: "real",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Episodes",
                keyColumn: "Id",
                keyValue: 1,
                column: "TmdbRating",
                value: null);

            migrationBuilder.UpdateData(
                table: "Videos",
                keyColumn: "Id",
                keyValue: 1,
                column: "TmdbRating",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TmdbRating",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "TmdbRating",
                table: "Episodes");
        }
    }
}
