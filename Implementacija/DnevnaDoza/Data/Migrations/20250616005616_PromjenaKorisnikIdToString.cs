using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class PromjenaKorisnikIdToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cijena",
                table: "StavkeNarudzbe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IDNarudzbe",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Ukupno",
                table: "StavkeNarudzbe",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cijena",
                table: "Korpa",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KorisnikId",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_IDKorisnik",
                table: "Korpa",
                column: "IDKorisnik");

            migrationBuilder.CreateIndex(
                name: "IX_Korpa_ProizvodId",
                table: "Korpa",
                column: "ProizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Korisnik_IDKorisnik",
                table: "Korpa",
                column: "IDKorisnik",
                principalTable: "Korisnik",
                principalColumn: "IDKorisnik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Korpa_Proizvod_ProizvodId",
                table: "Korpa",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Korisnik_IDKorisnik",
                table: "Korpa");

            migrationBuilder.DropForeignKey(
                name: "FK_Korpa_Proizvod_ProizvodId",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_IDKorisnik",
                table: "Korpa");

            migrationBuilder.DropIndex(
                name: "IX_Korpa_ProizvodId",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "IDNarudzbe",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "Ukupno",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "Korpa");
        }
    }
}
