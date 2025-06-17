using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class Korisnik
    {
        [Key]
        public int IDKorisnik { get; set; }

        [Required(ErrorMessage = "Ime je obavezno.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Ime mora početi velikim slovom i sadržavati samo slova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ]*$", ErrorMessage = "Prezime mora početi velikim slovom i sadržavati samo slova.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Korisničko ime je obavezno.")]
        [RegularExpression(@"^[a-zA-Z0-9._-]{3,20}$", ErrorMessage = "Korisničko ime može sadržavati slova, brojeve, tačku, crtu i mora imati između 3 i 20 karaktera.")]
        public string KorisnickoIme { get; set; }

        public TipKorisnika TipKorisnika { get; set; }

        public DateOnly DatumZaposlenja { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Lozinka mora imati najmanje 8 karaktera.")]
      
        public string Lozinka { get; set; }

        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC mora imati tačno 3 cifre.")]
        public string CVC { get; set; }

        public DateOnly DatumIstekaKartice { get; set; }

        [RegularExpression(@"^\d{16}$", ErrorMessage = "Broj kartice mora imati tačno 16 cifara.")]
        public string BrojKartice { get; set; }

        [RegularExpression(@"^\+?\d{6,15}$", ErrorMessage = "Broj telefona mora sadržavati samo cifre, uz opcionalni '+' na početku.")]
        public string BrojTelefona { get; set; }

      
        public string MjestoStanovanja { get; set; }

        [RegularExpression(@"^\d{5}$", ErrorMessage = "Poštanski broj mora imati tačno 5 cifara.")]
        public string PostanskiBroj { get; set; }

       
        public string Adresa { get; set; }

        [Required]
  
        public string EMail { get; set; }

        [ForeignKey("Apoteka")]
        public int IDApoteke { get; set; }

        public bool EmailConfirmed { get; set; } = false;

        public string ConfirmationToken { get; set; }

        // public Apoteka Apoteka { get; set; }
    }
}
