using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class Korisnik
    {
        [Key]
        public string IDKorisnik { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public DateOnly DatumZaposlenja { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Lozinka mora imati najmanje 8 karaktera.")]
        public string Lozinka { get; set; }
        public string CVC { get; set; }
        public DateOnly DatumIstekaKartice { get; set; }
        public string BrojKartice { get; set; }
        public string BrojTelefona { get; set; }
        public string MjestoStanovanja { get; set; }
        public string PostanskiBroj { get; set; }
        public string Adresa { get; set; }

        [Required]
        [EmailAddress]
        public string EMail { get; set; }

        [ForeignKey("Apoteka")]
        public int IDApoteke { get; set; }

        public bool EmailConfirmed { get; set; } = false;
        public string ConfirmationToken { get; set; }

        // Navigacijsko svojstvo
        //public Apoteka Apoteka { get; set; }



    }
}
