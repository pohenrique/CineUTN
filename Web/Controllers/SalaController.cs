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
    public class SalaController : Controller
    {
        private readonly CineUTNContext _context;

        public SalaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Sala
        public async Task<IActionResult> Index()
        {
            ViewBag.SignIn = true;
            var cineUTNContext = _context.Salas.Include(s => s.Sonido).Include(s => s.Tipo);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Sala/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(s => s.Sonido)
                .Include(s => s.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // GET: Sala/Create
        public IActionResult Create()
        {
            ViewBag.SignIn = true;
            ViewData["SonidoRefId"] = new SelectList(_context.Sonidos, "Id", "Descripcion");
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion");
            return View();
        }

        // POST: Sala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,TipoRefId,SonidoRefId,FechaRegistro")] Sala sala)
        {
            ViewBag.SignIn = true;
            if (ModelState.IsValid)
            {
                _context.Add(sala);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SonidoRefId"] = new SelectList(_context.Sonidos, "Id", "Descripcion", sala.SonidoRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", sala.TipoRefId);
            return View(sala);
        }

        // GET: Sala/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }
            ViewData["SonidoRefId"] = new SelectList(_context.Sonidos, "Id", "Descripcion", sala.SonidoRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", sala.TipoRefId);
            return View(sala);
        }

        // POST: Sala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,TipoRefId,SonidoRefId,FechaRegistro")] Sala sala)
        {
            ViewBag.SignIn = true;
            if (id != sala.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sala);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaExists(sala.Id))
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
            ViewData["SonidoRefId"] = new SelectList(_context.Sonidos, "Id", "Descripcion", sala.SonidoRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", sala.TipoRefId);
            return View(sala);
        }

        // GET: Sala/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Salas == null)
            {
                return NotFound();
            }

            var sala = await _context.Salas
                .Include(s => s.Sonido)
                .Include(s => s.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sala == null)
            {
                return NotFound();
            }

            return View(sala);
        }

        // POST: Sala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.SignIn = true;
            if (_context.Salas == null)
            {
                return Problem("Entity set 'CineUTNContext.Salas'  is null.");
            }
            var sala = await _context.Salas.FindAsync(id);
            if (sala != null)
            {
                _context.Salas.Remove(sala);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaExists(int id)
        {
          return (_context.Salas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
