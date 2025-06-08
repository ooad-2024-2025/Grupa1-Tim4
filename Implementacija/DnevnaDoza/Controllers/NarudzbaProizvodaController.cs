using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnevnaDoza.Data;
using DnevnaDoza.Models;
using DnevnaDoza.Services;
using Microsoft.AspNetCore.Authorization;

namespace DnevnaDoza.Controllers
{


    [Authorize]
    public class NarudzbaProizvodaController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly EmailServis _emailService;

        public NarudzbaProizvodaController(ApplicationDbContext context, EmailServis emailServis)
        {
            _context = context;
            _emailService = emailServis;
        }

        // GET: NarudzbaProizvodas
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.NarudzbaProizvoda.ToListAsync());
        }*/

        public async Task<IActionResult> Index()
        {
            try
            {
                var narudzba = await _context.NarudzbaProizvoda.ToListAsync();
                return View(narudzba);
            }
            catch (Exception ex)
            {
                return Content("Greška prilikom učitavanja podatakaiz narudzbe: " + ex.Message);
            }
        }

        // GET: NarudzbaProizvodas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzbaProizvoda = await _context.NarudzbaProizvoda
                .FirstOrDefaultAsync(m => m.IDNarudzbe == id);
            if (narudzbaProizvoda == null)
            {
                return NotFound();
            }

            return View(narudzbaProizvoda);
        }

        // GET: NarudzbaProizvodas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NarudzbaProizvodas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDNarudzbe,BrojProizvoda,UkupnaCijena,JeObradjenaNarudzba,DatumNarudzbe,IDKorisnika,IDObradaNarudzbe")] NarudzbaProizvoda narudzbaProizvoda)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzbaProizvoda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(narudzbaProizvoda);
        }

        // GET: NarudzbaProizvodas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzbaProizvoda = await _context.NarudzbaProizvoda.FindAsync(id);
            if (narudzbaProizvoda == null)
            {
                return NotFound();
            }
            return View(narudzbaProizvoda);
        }

        // POST: NarudzbaProizvodas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDNarudzbe,BrojProizvoda,UkupnaCijena,JeObradjenaNarudzba,DatumNarudzbe,IDKorisnika,IDObradaNarudzbe")] NarudzbaProizvoda narudzbaProizvoda)
        {
            if (id != narudzbaProizvoda.IDNarudzbe)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzbaProizvoda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaProizvodaExists(narudzbaProizvoda.IDNarudzbe))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(narudzbaProizvoda);
        }

        // GET: NarudzbaProizvodas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzbaProizvoda = await _context.NarudzbaProizvoda
                .FirstOrDefaultAsync(m => m.IDNarudzbe == id);
            if (narudzbaProizvoda == null)
            {
                return NotFound();
            }

            return View(narudzbaProizvoda);
        }

        // POST: NarudzbaProizvodas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzbaProizvoda = await _context.NarudzbaProizvoda.FindAsync(id);
            if (narudzbaProizvoda != null)
            {
                _context.NarudzbaProizvoda.Remove(narudzbaProizvoda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            // Preuzmite podatke o porudžbini iz baze
            var order = await _context.NarudzbaProizvoda
                .Include(o => o.Korisnik) // Pretpostavlja se da je veza između korisnika i narudžbe definisana
                .FirstOrDefaultAsync(o => o.IDNarudzbe == orderId);

            if (order == null)
            {
                return NotFound("Porudžbina nije pronađena.");
            }

            // Generišite sadržaj računa
            string subject = "Vaš račun za kupovinu";
            string body = $"Poštovani {order.Korisnik.Ime},<br/><br/>" +
                          $"Hvala Vam na kupovini!<br/>" +
                          $"Ukupan iznos: {order.UkupnaCijena:C}.<br/>" +
                          $"Račun je priložen u ovom e-mailu.";

            // Pošaljite e-mail korisniku
            await _emailService.SendEmailAsync(order.Korisnik.EMail, subject, body);

            TempData["Message"] = "Kupovina završena! Račun je poslat na e-mail.";
            return RedirectToAction("Index");
        }


        private bool NarudzbaProizvodaExists(int id)
        {
            return _context.NarudzbaProizvoda.Any(e => e.IDNarudzbe == id);
        }
    }
}
