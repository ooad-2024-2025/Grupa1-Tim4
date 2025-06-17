using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class NarudzbaProizvoda
    {

        [Key]
        public int IDNarudzbe { get; set; }

        public int BrojProizvoda { get; set; }

        public double UkupnaCijena { get; set; }

        public bool JeObradjenaNarudzba { get; set; }

        public DateTime DatumNarudzbe { get; set; }

        [ForeignKey("Korisnik")]
        public string IDKorisnika { get; set; }

        [ForeignKey("ObradaNarudzbe")]
        public int IDObradaNarudzbe { get; set; }

        // Navigacijska svojstva
        public Korisnik Korisnik { get; set; }  // Veza sa korisnikom
        public ObradaNarudzbe ObradaNarudzbe { get; set; } // Veza sa obradom narudžbe

    }
}
