namespace DnevnaDoza.Models
{
    public class ERacun
    {
        int IDRacuna {  get; set; }
        int IDNarudzbe {  get; set; }
        String nazivZaposlenika {  get; set; }
        bool ponistenRacun {  get; set; }
        float iznostNarudzbe { get; set; }
        DateOnly datum {  get; set; }
        
    }
}
