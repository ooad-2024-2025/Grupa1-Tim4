using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DnevnaDoza.Controllers
{
    public class ChackOutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChackOutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetKorisnikId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: /ChackOuts
     
        public async Task<IActionResult> Index()
        {
            var korisnikId = GetKorisnikId();
            var stavke = await _context.ChackOut
                .Include(c => c.Proizvod)
                .Where(c => c.KorisnikId == korisnikId)
                .ToListAsync();

            return View(stavke); // obavezno napravi Views/ChackOuts/Index.cshtml
        }

        // POST: /ChackOuts/Dodaj

        [HttpPost]
        public async Task<IActionResult> Dodaj(int proizvodId)
        {
            var korisnikId = GetKorisnikId();
            var proizvod = await _context.Proizvod.FindAsync(proizvodId);
            if (proizvod == null)
                return NotFound();

            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.ProizvodId == proizvodId && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                stavka.Kolicina++;
            }
            else
            {
                stavka = new ChackOut
                {
                    ProizvodId = proizvodId,
                    Naziv = proizvod.Naziv,
                    Cijena = (decimal)proizvod.Cijena,
                    Kolicina = 1,
                    KorisnikId = korisnikId
                };
                _context.ChackOut.Add(stavka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: /ChackOuts/Izbrisi

        [HttpPost]
        public async Task<IActionResult> Izbrisi(int id)
        {
            var korisnikId = GetKorisnikId();
            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.Id == id && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                _context.ChackOut.Remove(stavka);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        // POST: /ChackOuts/Umanji
        [HttpPost]
        public async Task<IActionResult> Umanji(int id)
        {
            var korisnikId = GetKorisnikId();
            var stavka = await _context.ChackOut
                .FirstOrDefaultAsync(c => c.Id == id && c.KorisnikId == korisnikId);

            if (stavka != null)
            {
                if (stavka.Kolicina > 1)
                {
                    stavka.Kolicina--;
                }
                else
                {
                    _context.ChackOut.Remove(stavka);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteOrder(string Ime, string Prezime, string BrojTelefona, string Grad, string Adresa, string PostanskiBroj, string BrojKartice, string CVC, DateTime DatumIstekaKartice)
        {
            // Validacija podataka
            if (string.IsNullOrWhiteSpace(Ime) || string.IsNullOrWhiteSpace(Prezime) ||
                string.IsNullOrWhiteSpace(BrojTelefona) || string.IsNullOrWhiteSpace(Grad) ||
                string.IsNullOrWhiteSpace(Adresa) || string.IsNullOrWhiteSpace(PostanskiBroj) ||
                string.IsNullOrWhiteSpace(BrojKartice) || string.IsNullOrWhiteSpace(CVC) ||
                DatumIstekaKartice == default(DateTime))
            {
                TempData["Error"] = "Sva polja su obavezna.";
                return RedirectToAction("Index");
            }

            // Dohvatanje trenutnog korisničkog GUID-a
            var korisnikGuid = GetKorisnikId();
            if (string.IsNullOrEmpty(korisnikGuid))
            {
                TempData["Error"] = "Korisnik nije prijavljen.";
                return RedirectToAction("Index");
            }

            // Pronađi korisnika u bazi
            var korisnik = await _context.Korisnik
                .FirstOrDefaultAsync(k => k.KorisnickoIme == korisnikGuid); // Koristi stvarnu kolonu koja čuva GUID

            if (korisnik == null)
            {
                TempData["Error"] = "Korisnik nije pronađen.";
                return RedirectToAction("Index");
            }

            int korisnikId = korisnik.IDKorisnik;

            // Dohvatanje stavki iz korpe
            var stavke = await _context.ChackOut
                .Include(c => c.Proizvod)
                .Where(c => c.KorisnikId == korisnikGuid) // Koristi GUID za pretragu
                .ToListAsync();

            if (stavke == null || !stavke.Any())
            {
                TempData["Error"] = "Vaša korpa je prazna.";
                return RedirectToAction("Index");
            }

            // Izračunavanje ukupne cene
            double ukupnaCijena = stavke.Sum(s => (double)s.Cijena * s.Kolicina);

            // Kreiranje nove narudžbe
            var narudzba = new NarudzbaProizvoda
            {
                BrojProizvoda = stavke.Count,
                UkupnaCijena = ukupnaCijena,
                DatumNarudzbe = DateTime.Now,
                IDKorisnika = korisnikId, // Koristi int ID
                JeObradjenaNarudzba = false,

                // Dodavanje korisničkih podataka
               // Ime = Ime,
                //Prezime = Prezime,
                //BrojTelefona = BrojTelefona,
                //Grad = Grad,
              //  Adresa = Adresa,
              //  PostanskiBroj = PostanskiBroj,
               // BrojKartice = BrojKartice,
               // CVC = CVC,
               // DatumIstekaKartice = DatumIstekaKartice
            };

            // Dodavanje narudžbe u bazu
            _context.NarudzbaProizvoda.Add(narudzba);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Narudžba je uspešno završena!";
            return RedirectToAction("Index");
        }
    }
}
