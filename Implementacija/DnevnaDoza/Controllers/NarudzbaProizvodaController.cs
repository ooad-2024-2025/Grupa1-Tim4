using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnevnaDoza.Data;
using DnevnaDoza.Models;

namespace DnevnaDoza.Controllers
{
    public class NarudzbaProizvodaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NarudzbaProizvodaController(ApplicationDbContext context)
        {
            _context = context;
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

        private bool NarudzbaProizvodaExists(int id)
        {
            return _context.NarudzbaProizvoda.Any(e => e.IDNarudzbe == id);
        }
    }
}
