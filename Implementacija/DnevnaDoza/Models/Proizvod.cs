
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DnevnaDoza.Models
{
    public class Proizvod
    {
        [Key]
        public int ID { get; set; }

        public string Naziv { get; set; }

        public float Cijena { get; set; }

        public string Dostupnost { get; set; }

        [ForeignKey("Korpa")]
        public int IDKorpa { get; set; }

        public KategorijeProizvoda Kategorija { get; set; }

        // Navigacijsko svojstvo
       // public Korpa Korpa { get; set; }

        public Proizvod()
        {
        }
    }
}
