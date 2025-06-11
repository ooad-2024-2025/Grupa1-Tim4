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
    public class ChackOutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChackOutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChackOuts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ChackOut.Include(c => c.Proizvod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ChackOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chackOut = await _context.ChackOut
                .Include(c => c.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chackOut == null)
            {
                return NotFound();
            }

            return View(chackOut);
        }

        // GET: ChackOuts/Create
        public IActionResult Create()
        {
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "ID", "ID");
            return View();
        }

        // POST: ChackOuts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProizvodId,Naziv,Cijena,Kolicina")] ChackOut chackOut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chackOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "ID", "ID", chackOut.ProizvodId);
            return View(chackOut);
        }

        // GET: ChackOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chackOut = await _context.ChackOut.FindAsync(id);
            if (chackOut == null)
            {
                return NotFound();
            }
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "ID", "ID", chackOut.ProizvodId);
            return View(chackOut);
        }

        // POST: ChackOuts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProizvodId,Naziv,Cijena,Kolicina")] ChackOut chackOut)
        {
            if (id != chackOut.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chackOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChackOutExists(chackOut.Id))
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
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "ID", "ID", chackOut.ProizvodId);
            return View(chackOut);
        }

        // GET: ChackOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chackOut = await _context.ChackOut
                .Include(c => c.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chackOut == null)
            {
                return NotFound();
            }

            return View(chackOut);
        }

        // POST: ChackOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chackOut = await _context.ChackOut.FindAsync(id);
            if (chackOut != null)
            {
                _context.ChackOut.Remove(chackOut);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChackOutExists(int id)
        {
            return _context.ChackOut.Any(e => e.Id == id);
        }
    }
}
