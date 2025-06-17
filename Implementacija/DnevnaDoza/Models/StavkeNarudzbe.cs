using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class StavkeNarudzbe
    {
        [Key]
        public int IDStavkeNarudzbe { get; set; }

        [ForeignKey("Proizvod")]
        public int IDProizvoda { get; set; }

        [ForeignKey("NarudzbaProizvoda")]
        public int IDNarudzba { get; set; }

        [ForeignKey("ERacun")]
        public int IDEMail { get; set; }

        public string NazivProizvoda { get; set; }

        public int Kolicina { get; set; }

        public float CijenaProizvoda { get; set; }

        public string Opis { get; set; }
        public int IDNarudzbe { get; internal set; }
        public int ProizvodId { get; internal set; }
        public decimal Ukupno { get; internal set; }
        public decimal Cijena { get; internal set; }

        // Navigacijska svojstva
        // public Proizvod Proizvod { get; set; }
        //public NarudzbaProizvoda NarudzbaProizvoda { get; set; }
        //public ERacun ERacun { get; set; }

    }
}
