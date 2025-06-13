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

        [ForeignKey("Korisnik")]
        public int IDKorisnik { get; set; }

        [ForeignKey("NarudzbaProizvoda")]
        public int IDNarudzbe { get; set; }

        // Navigacijska svojstva
        public virtual Proizvod Proizvod { get; set; }
        // public Korisnik Korisnik { get; set; }
        //public NarudzbaProizvoda NarudzbaProizvoda { get; set; }
    }
}
