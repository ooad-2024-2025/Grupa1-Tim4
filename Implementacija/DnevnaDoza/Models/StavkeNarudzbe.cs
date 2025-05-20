namespace DnevnaDoza.Models
{
    public class StavkeNarudzbe
    { 
        int IDStavkeNarudzbe {  get; set; }
        int IDProizvoda { get; set; }
        int IDNarudzba { get; set; }
        int IDEMail { get; set; }
        String nazivProizvoda { get; set; }
        int kolicina { get; set; }
        float cijenaProizvoda { get; set; }
        String opis {  get; set; }
           
    }
}
