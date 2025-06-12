using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmationToKorisnik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmationToken",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Korisnik",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationToken",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Korisnik");
        }
    }
}
