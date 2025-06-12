using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class korpa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "ChackOut",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "ChackOut");
        }
    }
}
