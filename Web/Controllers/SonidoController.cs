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
    public class SonidoController : Controller
    {
        private readonly CineUTNContext _context;

        public SonidoController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Sonido
        public async Task<IActionResult> Index()
        {
             
            return _context.Sonidos != null ? 
                          View(await _context.Sonidos.ToListAsync()) :
                          Problem("Entity set 'CineUTNContext.Sonidos'  is null.");
        }

        // GET: Sonido/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             
            if (id == null || _context.Sonidos == null)
            {
                return NotFound();
            }

            var sonido = await _context.Sonidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sonido == null)
            {
                return NotFound();
            }

            return View(sonido);
        }

        // GET: Sonido/Create
        public IActionResult Create()
        {
             
            return View();
        }

        // POST: Sonido/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaRegistro")] Sonido sonido)
        {
             
            if (ModelState.IsValid)
            {
                _context.Add(sonido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sonido);
        }

        // GET: Sonido/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             
            if (id == null || _context.Sonidos == null)
            {
                return NotFound();
            }

            var sonido = await _context.Sonidos.FindAsync(id);
            if (sonido == null)
            {
                return NotFound();
            }
            return View(sonido);
        }

        // POST: Sonido/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaRegistro")] Sonido sonido)
        {
             
            if (id != sonido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sonido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SonidoExists(sonido.Id))
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
            return View(sonido);
        }

        // GET: Sonido/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
             
            if (id == null || _context.Sonidos == null)
            {
                return NotFound();
            }

            var sonido = await _context.Sonidos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sonido == null)
            {
                return NotFound();
            }

            return View(sonido);
        }

        // POST: Sonido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             
            if (_context.Sonidos == null)
            {
                return Problem("Entity set 'CineUTNContext.Sonidos'  is null.");
            }
            var sonido = await _context.Sonidos.FindAsync(id);
            if (sonido != null)
            {
                _context.Sonidos.Remove(sonido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SonidoExists(int id)
        {
          return (_context.Sonidos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
