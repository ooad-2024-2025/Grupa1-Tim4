
namespace DnevnaDoza.Models
{
    public class Proizvod
    {
        int ID {  get; set; }
        String naziv {  get; set; }
        float cijena {  get; set; }
        String dostupnost {  get; set; }
        int IDKorpa {  get; set; }
        KategorijeProizvoda kategorija {  get; set; }
        public Proizvod() {


    }
}
