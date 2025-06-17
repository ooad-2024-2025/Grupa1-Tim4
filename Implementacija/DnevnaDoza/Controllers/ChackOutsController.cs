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
    }
}
