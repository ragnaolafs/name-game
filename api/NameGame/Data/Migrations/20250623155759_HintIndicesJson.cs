using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NameGame.Data.Migrations
{
    /// <inheritdoc />
    public partial class HintIndicesJson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HintIndicesJson",
                table: "Guesses",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnableHints",
                table: "Games",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HintIndicesJson",
                table: "Guesses");

            migrationBuilder.DropColumn(
                name: "EnableHints",
                table: "Games");
        }
    }
}
