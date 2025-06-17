using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnevnaDoza.Controllers
{
    public class ProizvodController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProizvodController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Proizvod
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
          {
              try
              {
                  var proizvodi = await _context.Proizvod.ToListAsync();
                  return View(proizvodi);
              }
              catch (Exception ex)
              {
                  return Content("Greška prilikom učitavanja podataka: " + ex.Message);
              }
          }
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var proizvod = await _context.Proizvod
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proizvod == null) return NotFound();

            return View(proizvod);
        }

        // GET: Proizvod/Create
        public IActionResult Create()
        {
            ViewBag.Kategorije = Enum.GetValues(typeof(KategorijeProizvoda))
                .Cast<KategorijeProizvoda>()
                .Select(k => new SelectListItem
                {
                    Text = k.ToString(),
                    Value = k.ToString()
                }).ToList();

            return View();
        }

        // POST: Proizvod/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naziv,Cijena,Dostupnost,IDKorpa,Kategorija")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Kategorije = Enum.GetValues(typeof(KategorijeProizvoda))
                .Cast<KategorijeProizvoda>()
                .Select(k => new SelectListItem
                {
                    Text = k.ToString(),
                    Value = k.ToString()
                }).ToList();

            return View(proizvod);
        }

        // GET: Proizvod/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null) return NotFound();

            ViewBag.Kategorije = Enum.GetValues(typeof(KategorijeProizvoda))
                .Cast<KategorijeProizvoda>()
                .Select(k => new SelectListItem
                {
                    Text = k.ToString(),
                    Value = k.ToString()
                }).ToList();

            return View(proizvod);
        }

        // POST: Proizvod/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naziv,Cijena,Dostupnost,IDKorpa,Kategorija")] Proizvod proizvod)
        {
            if (id != proizvod.ID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.ID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Kategorije = Enum.GetValues(typeof(KategorijeProizvoda))
                .Cast<KategorijeProizvoda>()
                .Select(k => new SelectListItem
                {
                    Text = k.ToString(),
                    Value = k.ToString()
                }).ToList();

            return View(proizvod);
        }

        // GET: Proizvod/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var proizvod = await _context.Proizvod
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proizvod == null) return NotFound();

            return View(proizvod);
        }

        // POST: Proizvod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvod.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvod.Any(e => e.ID == id);
        }
    }
}
