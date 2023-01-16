using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using parafia2.Models.DataLayer;

namespace parafia2.Controllers
{
    public class StanowiskasController : Controller
    {
        private readonly ParafiaContext _context;

        public StanowiskasController(ParafiaContext context)
        {
            _context = context;
        }

        // GET: Stanowiskas
        public async Task<IActionResult> Index()
        {
              return _context.Stanowiskas != null ? 
                          View(await _context.Stanowiskas.ToListAsync()) :
                          Problem("Entity set 'ParafiaContext.Stanowiskas'  is null.");
        }

        // GET: Stanowiskas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Stanowiskas == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiskas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stanowiska == null)
            {
                return NotFound();
            }

            return View(stanowiska);
        }

        // GET: Stanowiskas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stanowiskas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NazwaStanowiska")] Stanowiska stanowiska)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stanowiska);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stanowiska);
        }

        // GET: Stanowiskas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Stanowiskas == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiskas.FindAsync(id);
            if (stanowiska == null)
            {
                return NotFound();
            }
            return View(stanowiska);
        }

        // POST: Stanowiskas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NazwaStanowiska")] Stanowiska stanowiska)
        {
            if (id != stanowiska.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stanowiska);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StanowiskaExists(stanowiska.Id))
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
            return View(stanowiska);
        }

        // GET: Stanowiskas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Stanowiskas == null)
            {
                return NotFound();
            }

            var stanowiska = await _context.Stanowiskas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stanowiska == null)
            {
                return NotFound();
            }

            return View(stanowiska);
        }

        // POST: Stanowiskas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Stanowiskas == null)
            {
                return Problem("Entity set 'ParafiaContext.Stanowiskas'  is null.");
            }
            var stanowiska = await _context.Stanowiskas.FindAsync(id);
            if (stanowiska != null)
            {
                _context.Stanowiskas.Remove(stanowiska);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StanowiskaExists(int id)
        {
          return (_context.Stanowiskas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
