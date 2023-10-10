using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;

namespace Web.Controllers
{
    public class CondicionPagoController : Controller
    {
        private readonly CineUTNContext _context;

        public CondicionPagoController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: CondicionPago
        public async Task<IActionResult> Index()
        {
            ViewBag.SignIn = true;
            return _context.CondicionPagos != null ? 
                          View(await _context.CondicionPagos.ToListAsync()) :
                          Problem("Entity set 'CineUTNContext.CondicionPagos'  is null.");
        }

        // GET: CondicionPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.CondicionPagos == null)
            {
                return NotFound();
            }

            var condicionPago = await _context.CondicionPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condicionPago == null)
            {
                return NotFound();
            }

            return View(condicionPago);
        }

        // GET: CondicionPago/Create
        public IActionResult Create()
        {
            ViewBag.SignIn = true;
            return View();
        }

        // POST: CondicionPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] CondicionPago condicionPago)
        {
            ViewBag.SignIn = true;
            if (ModelState.IsValid)
            {
                _context.Add(condicionPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(condicionPago);
        }

        // GET: CondicionPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.CondicionPagos == null)
            {
                return NotFound();
            }

            var condicionPago = await _context.CondicionPagos.FindAsync(id);
            if (condicionPago == null)
            {
                return NotFound();
            }
            return View(condicionPago);
        }

        // POST: CondicionPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] CondicionPago condicionPago)
        {
            ViewBag.SignIn = true;
            if (id != condicionPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(condicionPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CondicionPagoExists(condicionPago.Id))
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
            return View(condicionPago);
        }

        // GET: CondicionPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.CondicionPagos == null)
            {
                return NotFound();
            }

            var condicionPago = await _context.CondicionPagos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (condicionPago == null)
            {
                return NotFound();
            }

            return View(condicionPago);
        }

        // POST: CondicionPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.SignIn = true;
            if (_context.CondicionPagos == null)
            {
                return Problem("Entity set 'CineUTNContext.CondicionPagos'  is null.");
            }
            var condicionPago = await _context.CondicionPagos.FindAsync(id);
            if (condicionPago != null)
            {
                _context.CondicionPagos.Remove(condicionPago);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CondicionPagoExists(int id)
        {
          return (_context.CondicionPagos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
