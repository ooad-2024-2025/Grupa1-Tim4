using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class provjeraKrosinika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaProizvoda_IDKorisnika",
                table: "NarudzbaProizvoda",
                column: "IDKorisnika");

            migrationBuilder.CreateIndex(
                name: "IX_NarudzbaProizvoda_IDObradaNarudzbe",
                table: "NarudzbaProizvoda",
                column: "IDObradaNarudzbe");

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaProizvoda_Korisnik_IDKorisnika",
                table: "NarudzbaProizvoda",
                column: "IDKorisnika",
                principalTable: "Korisnik",
                principalColumn: "IDKorisnik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NarudzbaProizvoda_ObradaNarudzbe_IDObradaNarudzbe",
                table: "NarudzbaProizvoda",
                column: "IDObradaNarudzbe",
                principalTable: "ObradaNarudzbe",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaProizvoda_Korisnik_IDKorisnika",
                table: "NarudzbaProizvoda");

            migrationBuilder.DropForeignKey(
                name: "FK_NarudzbaProizvoda_ObradaNarudzbe_IDObradaNarudzbe",
                table: "NarudzbaProizvoda");

            migrationBuilder.DropIndex(
                name: "IX_NarudzbaProizvoda_IDKorisnika",
                table: "NarudzbaProizvoda");

            migrationBuilder.DropIndex(
                name: "IX_NarudzbaProizvoda_IDObradaNarudzbe",
                table: "NarudzbaProizvoda");
        }
    }
}
