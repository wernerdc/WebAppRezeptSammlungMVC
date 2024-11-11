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
    public class LebensmittelsController : Controller
    {
        private readonly WebAppRezeptSammlungMVCContext _context;

        public LebensmittelsController(WebAppRezeptSammlungMVCContext context)
        {
            _context = context;
        }

        // GET: Lebensmittels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lebensmittel.ToListAsync());
        }

        // GET: Lebensmittels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lebensmittel = await _context.Lebensmittel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lebensmittel == null)
            {
                return NotFound();
            }

            return View(lebensmittel);
        }

        // GET: Lebensmittels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lebensmittels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bezeichnung,Kategorie")] Lebensmittel lebensmittel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lebensmittel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lebensmittel);
        }

        // GET: Lebensmittels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lebensmittel = await _context.Lebensmittel.FindAsync(id);
            if (lebensmittel == null)
            {
                return NotFound();
            }
            return View(lebensmittel);
        }

        // POST: Lebensmittels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bezeichnung,Kategorie")] Lebensmittel lebensmittel)
        {
            if (id != lebensmittel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lebensmittel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LebensmittelExists(lebensmittel.Id))
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
            return View(lebensmittel);
        }

        // GET: Lebensmittels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lebensmittel = await _context.Lebensmittel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lebensmittel == null)
            {
                return NotFound();
            }

            return View(lebensmittel);
        }

        // POST: Lebensmittels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lebensmittel = await _context.Lebensmittel.FindAsync(id);
            if (lebensmittel != null)
            {
                _context.Lebensmittel.Remove(lebensmittel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LebensmittelExists(int id)
        {
            return _context.Lebensmittel.Any(e => e.Id == id);
        }
    }
}
