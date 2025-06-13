using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DnevnaDoza.Data;
using DnevnaDoza.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DnevnaDoza.Services;

namespace DnevnaDoza.Controllers
{
    [Authorize]
    public class KorpaController : Controller
    {
        private readonly ApplicationDbContext _context;
       

        public KorpaController(ApplicationDbContext context)
        {
            _context = context;
           
        }

        // GET: Korpas
         public async Task<IActionResult> Index()
         {
             return View(await _context.Korpa.ToListAsync());
         }
       /* public async Task<IActionResult> Index()
        {
            try
            {
                var proizvodi = await _context.Korpa.ToListAsync();
                return View(proizvodi);
            }
            catch (Exception ex)
            {
                return Content("Greška prilikom učitavanja podataka iz korpe: " + ex.Message);
            }
        }*/


        // GET: Korpas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpa = await _context.Korpa
                .FirstOrDefaultAsync(m => m.ID == id);
            if (korpa == null)
            {
                return NotFound();
            }

            return View(korpa);
        }

        // GET: Korpas/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: Korpas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StanjeKorpe,BrojProizvoda,PotvrdjenaNarudzba,UkupanIznos,IDKorisnik,IDNarudzbe")] Korpa korpa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(korpa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(korpa);
        }

        // GET: Korpas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpa = await _context.Korpa.FindAsync(id);
            if (korpa == null)
            {
                return NotFound();
            }
            return View(korpa);
        }

        // POST: Korpas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StanjeKorpe,BrojProizvoda,PotvrdjenaNarudzba,UkupanIznos,IDKorisnik,IDNarudzbe")] Korpa korpa)
        {
            if (id != korpa.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(korpa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KorpaExists(korpa.ID))
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
            return View(korpa);
        }

        // GET: Korpas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var korpa = await _context.Korpa
                .FirstOrDefaultAsync(m => m.ID == id);
            if (korpa == null)
            {
                return NotFound();
            }

            return View(korpa);
        }


      
     

        // POST: Korpas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var korpa = await _context.Korpa.FindAsync(id);
            if (korpa != null)
            {
                _context.Korpa.Remove(korpa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KorpaExists(int id)
        {
            return _context.Korpa.Any(e => e.ID == id);
        }
    }
}
