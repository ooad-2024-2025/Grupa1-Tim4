using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{

    public class ERacun
    {
        [Key]
        public int IDRacuna { get; set; } // Primary key

        [ForeignKey("Narudzba")]
        public int IDNarudzbe { get; set; } // Foreign key

        public string NazivZaposlenika { get; set; }

        public bool PonistenRacun { get; set; }

        public float IznosNarudzbe { get; set; }

        public DateOnly Datum { get; set; }

        // Navigacijska veza ka Narudzba entitetu
       // public NarudzbaProizvoda NarudzbaProizvoda { get; set; }
    }

}
