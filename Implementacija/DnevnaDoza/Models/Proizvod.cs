using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class Proizvod
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan.")]
        [RegularExpression(@"^[A-Z][a-zA-ZčćžšđČĆŽŠĐ\s\-]{1,50}$", ErrorMessage = "Naziv mora početi velikim slovom i sadržavati samo slova, razmake ili crtice.")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Cijena je obavezna.")]
   
        public float Cijena { get; set; }

        [Required(ErrorMessage = "Dostupnost je obavezna.")]
      
        public string Dostupnost { get; set; }

        [ForeignKey("Korpa")]
        public int IDKorpa { get; set; }

        [Required(ErrorMessage = "Kategorija je obavezna.")]
        public KategorijeProizvoda Kategorija { get; set; }

        // Navigacijsko svojstvo
        // public Korpa Korpa { get; set; }

        public Proizvod()
        {
        }
    }
}
