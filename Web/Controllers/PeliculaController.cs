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
    public class PeliculaController : Controller
    {
        private readonly CineUTNContext _context;

        public PeliculaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Pelicula
        public async Task<IActionResult> Index()
        {
            ViewBag.SignIn = true;
            var cineUTNContext = _context.Peliculas.Include(p => p.Genero).Include(p => p.Tipo);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Pelicula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Pelicula/Create
        public IActionResult Create()
        {
            ViewBag.SignIn = true;
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion");
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion");
            return View();
        }

        // POST: Pelicula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,ImagemPelicula,Duracion,Clasificacion,GeneroRefId,TipoRefId,Subtitulada,FechaEstreno,FechaRegistro")] Pelicula pelicula)
        {
            ViewBag.SignIn = true;
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", pelicula.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // GET: Pelicula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", pelicula.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // POST: Pelicula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,ImagemPelicula,Duracion,Clasificacion,GeneroRefId,TipoRefId,Subtitulada,FechaEstreno,FechaRegistro")] Pelicula pelicula)
        {
            ViewBag.SignIn = true;
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
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
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", pelicula.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", pelicula.TipoRefId);
            return View(pelicula);
        }

        // GET: Pelicula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.SignIn = true;
            if (id == null || _context.Peliculas == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Genero)
                .Include(p => p.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Pelicula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.SignIn = true;
            if (_context.Peliculas == null)
            {
                return Problem("Entity set 'CineUTNContext.Peliculas'  is null.");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                _context.Peliculas.Remove(pelicula);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
          return (_context.Peliculas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
