using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrvaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apoteka",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoteka", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ERacun",
                columns: table => new
                {
                    IDRacuna = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDNarudzbe = table.Column<int>(type: "int", nullable: false),
                    NazivZaposlenika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PonistenRacun = table.Column<bool>(type: "bit", nullable: false),
                    IznosNarudzbe = table.Column<float>(type: "real", nullable: false),
                    Datum = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ERacun", x => x.IDRacuna);
                });

            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    IDKorisnik = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.IDKorisnik);
                });

            migrationBuilder.CreateTable(
                name: "Korpa",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpa", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NarudzbaProizvoda",
                columns: table => new
                {
                    IDNarudzbe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brojProizvoda = table.Column<int>(type: "int", nullable: false),
                    ukupnaCijena = table.Column<double>(type: "float", nullable: false),
                    datumNarudzbe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IDKorisnika = table.Column<int>(type: "int", nullable: false),
                    IDObradaNarudzbe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NarudzbaProizvoda", x => x.IDNarudzbe);
                });

            migrationBuilder.CreateTable(
                name: "ObradaNarudzbe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObradaNarudzbe", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Proizvod",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proizvod", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "StavkeNarudzbe",
                columns: table => new
                {
                    IDStavkeNarudzbe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkeNarudzbe", x => x.IDStavkeNarudzbe);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apoteka");

            migrationBuilder.DropTable(
                name: "ERacun");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Korpa");

            migrationBuilder.DropTable(
                name: "NarudzbaProizvoda");

            migrationBuilder.DropTable(
                name: "ObradaNarudzbe");

            migrationBuilder.DropTable(
                name: "Proizvod");

            migrationBuilder.DropTable(
                name: "StavkeNarudzbe");
        }
    }
}
