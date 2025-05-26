using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnevnaDoza.Models
{
    public class ObradaNarudzbe
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("NarudzbaProizvoda")]
        public int IDNarudzbe { get; set; }

        [ForeignKey("Zaposlenik")]
        public int IDZaposlenika { get; set; }

        public bool JeObradjen { get; set; }

        // Navigacijska svojstva
     //   public NarudzbaProizvoda NarudzbaProizvoda { get; set; }
      //  public Korisnik Zaposlenik { get; set; }
    }
}
