using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppRezeptSammlungMVC.Data;
using WebAppRezeptSammlungMVC.Models;

namespace WebAppRezeptSammlungMVC.Controllers
{
    public class ZutatsController : Controller
    {
        private readonly WebAppRezeptSammlungMVCContext _context;

        public ZutatsController(WebAppRezeptSammlungMVCContext context)
        {
            _context = context;
        }

        // GET: Zutats
        public async Task<IActionResult> Index()
        {
            var webAppRezeptSammlungMVCContext = _context.Zutat.Include(z => z.Lebensmittel).Include(z => z.Rezept);
            return View(await webAppRezeptSammlungMVCContext.ToListAsync());
        }

        // GET: Zutats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zutat = await _context.Zutat
                .Include(z => z.Lebensmittel)
                .Include(z => z.Rezept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zutat == null)
            {
                return NotFound();
            }

            return View(zutat);
        }

        // GET: Zutats/Create
        public IActionResult Create()
        {
            ViewData["LebensmittelId"] = new SelectList(_context.Lebensmittel, "Id", "Bezeichnung");
            ViewData["RezeptId"] = new SelectList(_context.Rezept, "Id", "Bezeichnung");
            return View();
        }

        // POST: Zutats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RezeptId,LebensmittelId,Menge,Einheit")] Zutat zutat)    // why delete id?
        {
            if (ModelState.IsValid)
            {
                _context.Add(zutat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LebensmittelId"] = new SelectList(_context.Lebensmittel, "Id", "Bezeichnung", zutat.LebensmittelId);
            ViewData["RezeptId"] = new SelectList(_context.Rezept, "Id", "Bezeichnung", zutat.RezeptId);
            return View(zutat);
        }

        // GET: Zutats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zutat = await _context.Zutat
                .Include(z => z.Lebensmittel)
                .Include(z => z.Rezept)
                .FirstOrDefaultAsync(m => m.Id == id);
            //var zutat = await _context.Zutat.FindAsync(id);
            if (zutat == null)
            {
                return NotFound();
            }
            ViewData["LebensmittelId"] = new SelectList(_context.Lebensmittel, "Id", "Bezeichnung", zutat.LebensmittelId);
            ViewData["RezeptId"] = new SelectList(_context.Rezept, "Id", "Bezeichnung", zutat.RezeptId);
            return View(zutat);
        }

        // POST: Zutats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RezeptId,LebensmittelId,Menge,Einheit")] Zutat zutat)
        {
            if (id != zutat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zutat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZutatExists(zutat.Id))
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
            ViewData["LebensmittelId"] = new SelectList(_context.Lebensmittel, "Id", "Bezeichnung", zutat.LebensmittelId);
            ViewData["RezeptId"] = new SelectList(_context.Rezept, "Id", "Bezeichnung", zutat.RezeptId);
            return View(zutat);
        }

        // GET: Zutats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zutat = await _context.Zutat
                .Include(z => z.Lebensmittel)
                .Include(z => z.Rezept)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zutat == null)
            {
                return NotFound();
            }

            return View(zutat);
        }

        // POST: Zutats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zutat = await _context.Zutat.FindAsync(id);
            if (zutat != null)
            {
                _context.Zutat.Remove(zutat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZutatExists(int id)
        {
            return _context.Zutat.Any(e => e.Id == id);
        }
    }
}
