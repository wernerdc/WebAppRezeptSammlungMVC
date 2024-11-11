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
    public class RezeptsController : Controller
    {
        private readonly WebAppRezeptSammlungMVCContext _context;

        public RezeptsController(WebAppRezeptSammlungMVCContext context)
        {
            _context = context;
        }

        // GET: Rezepts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rezept.ToListAsync());
        }

        // GET: Rezepts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezept = await _context.Rezept
                .Include(r => r.Zutaten)                // erweiterung für Zutaten
                .ThenInclude(p => p.Lebensmittel)       // erweiterung für Zutaten
                .AsNoTracking()                         // erweiterung für Zutaten
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezept == null)
            {
                return NotFound();
            }

            return View(rezept);
        }

        // GET: Rezepts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rezepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Beschreibung,Bezeichnung,Zubereitung,Zubereitungszeit")] Rezept rezept)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rezept);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rezept);
        }

        // GET: Rezepts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezept = await _context.Rezept.FindAsync(id);
            if (rezept == null)
            {
                return NotFound();
            }
            return View(rezept);
        }

        // POST: Rezepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Beschreibung,Bezeichnung,Zubereitung,Zubereitungszeit")] Rezept rezept)
        {
            if (id != rezept.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezept);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezeptExists(rezept.Id))
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
            return View(rezept);
        }

        // GET: Rezepts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezept = await _context.Rezept
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rezept == null)
            {
                return NotFound();
            }

            return View(rezept);
        }

        // POST: Rezepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezept = await _context.Rezept.FindAsync(id);
            if (rezept != null)
            {
                _context.Rezept.Remove(rezept);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezeptExists(int id)
        {
            return _context.Rezept.Any(e => e.Id == id);
        }

        public IActionResult MyAction() 
        { 
            return View(); 
        }
    }
}
