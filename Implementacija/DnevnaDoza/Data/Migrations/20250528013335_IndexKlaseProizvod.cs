using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DnevnaDoza.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndexKlaseProizvod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ukupnaCijena",
                table: "NarudzbaProizvoda",
                newName: "UkupnaCijena");

            migrationBuilder.RenameColumn(
                name: "datumNarudzbe",
                table: "NarudzbaProizvoda",
                newName: "DatumNarudzbe");

            migrationBuilder.RenameColumn(
                name: "brojProizvoda",
                table: "NarudzbaProizvoda",
                newName: "BrojProizvoda");

            migrationBuilder.AddColumn<float>(
                name: "CijenaProizvoda",
                table: "StavkeNarudzbe",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "IDEMail",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDNarudzba",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDProizvoda",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "StavkeNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NazivProizvoda",
                table: "StavkeNarudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Opis",
                table: "StavkeNarudzbe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "Cijena",
                table: "Proizvod",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Dostupnost",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IDKorpa",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kategorija",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IDNarudzbe",
                table: "ObradaNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDZaposlenika",
                table: "ObradaNarudzbe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "JeObradjen",
                table: "ObradaNarudzbe",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JeObradjenaNarudzba",
                table: "NarudzbaProizvoda",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "BrojProizvoda",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDKorisnik",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IDNarudzbe",
                table: "Korpa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PotvrdjenaNarudzba",
                table: "Korpa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "StanjeKorpe",
                table: "Korpa",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "UkupanIznos",
                table: "Korpa",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrojKartice",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CVC",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DatumIstekaKartice",
                table: "Korisnik",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DatumZaposlenja",
                table: "Korisnik",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IDApoteke",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "KorisnickoIme",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lozinka",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MjestoStanovanja",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostanskiBroj",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipKorisnika",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Apoteka",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EMail",
                table: "Apoteka",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Apoteka",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RadnoVrijeme",
                table: "Apoteka",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Telefon",
                table: "Apoteka",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CijenaProizvoda",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "IDEMail",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "IDNarudzba",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "IDProizvoda",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "NazivProizvoda",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "Opis",
                table: "StavkeNarudzbe");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Dostupnost",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "IDKorpa",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Kategorija",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "IDNarudzbe",
                table: "ObradaNarudzbe");

            migrationBuilder.DropColumn(
                name: "IDZaposlenika",
                table: "ObradaNarudzbe");

            migrationBuilder.DropColumn(
                name: "JeObradjen",
                table: "ObradaNarudzbe");

            migrationBuilder.DropColumn(
                name: "JeObradjenaNarudzba",
                table: "NarudzbaProizvoda");

            migrationBuilder.DropColumn(
                name: "BrojProizvoda",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "IDKorisnik",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "IDNarudzbe",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "PotvrdjenaNarudzba",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "StanjeKorpe",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "UkupanIznos",
                table: "Korpa");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojKartice",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "CVC",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "DatumIstekaKartice",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "DatumZaposlenja",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "IDApoteke",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "KorisnickoIme",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Lozinka",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "MjestoStanovanja",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "PostanskiBroj",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "TipKorisnika",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Apoteka");

            migrationBuilder.DropColumn(
                name: "EMail",
                table: "Apoteka");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Apoteka");

            migrationBuilder.DropColumn(
                name: "RadnoVrijeme",
                table: "Apoteka");

            migrationBuilder.DropColumn(
                name: "Telefon",
                table: "Apoteka");

            migrationBuilder.RenameColumn(
                name: "UkupnaCijena",
                table: "NarudzbaProizvoda",
                newName: "ukupnaCijena");

            migrationBuilder.RenameColumn(
                name: "DatumNarudzbe",
                table: "NarudzbaProizvoda",
                newName: "datumNarudzbe");

            migrationBuilder.RenameColumn(
                name: "BrojProizvoda",
                table: "NarudzbaProizvoda",
                newName: "brojProizvoda");
        }
    }
}
