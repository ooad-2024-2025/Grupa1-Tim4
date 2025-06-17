using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class Korpa
    {
        [Key]
        public int ID { get; set; }

        public bool StanjeKorpe { get; set; }

        public int BrojProizvoda { get; set; }

        public bool PotvrdjenaNarudzba { get; set; }

        public float UkupanIznos { get; set; }
        [Required]
        public int ProizvodId { get; set; }

        [ForeignKey("ProizvodId")]
        public Proizvod Proizvod { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cijena { get; set; }

        public int Kolicina { get; set; } = 1;

        [Required]
        public string IDKorisnik { get; set; }

        [ForeignKey("IDKorisnik")]
        public Korisnik Korisnik { get; set; }

     //   [ForeignKey("Korisnik")]
       // public int IDKorisnik { get; set; }

        [ForeignKey("NarudzbaProizvoda")]
        public int IDNarudzbe { get; set; }
        public int KorisnikId { get; internal set; }

        // Navigacijska svojstva
        // public Korisnik Korisnik { get; set; }
        //public NarudzbaProizvoda NarudzbaProizvoda { get; set; }
    }
}
