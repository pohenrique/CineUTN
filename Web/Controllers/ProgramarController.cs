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
    public class ProgramarController : Controller
    {
        private readonly CineUTNContext _context;

        public ProgramarController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Programar
        public async Task<IActionResult> Index()
        {
             
            var cineUTNContext = _context.Programaciones
                .Include(p => p.Pelicula)
                .Include(p => p.Pelicula.Tipo)
                .Include(p => p.Sala)
                .Include(p => p.Sala.Tipo)
                .Include(p => p.Tarifa1)
                .Include(p => p.Tarifa1.ListaPrecio)
                .Include(p => p.Tarifa2)
                .Include(p => p.Tarifa2.ListaPrecio)
                .Include(p => p.Tarifa3)
                .Include(p => p.Tarifa3.ListaPrecio)
                .Include(p => p.Tarifa4)
                .Include(p => p.Tarifa4.ListaPrecio)
                .Include(p => p.Tarifa5)
                .Include(p => p.Tarifa5.ListaPrecio)
                .Include(p => p.Tarifa6)
                .Include(p => p.Tarifa6.ListaPrecio);
            

            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Programar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             
            if (id == null || _context.Programaciones == null)
            {
                return NotFound();
            }

            var programar = await _context.Programaciones
                .Include(p => p.Pelicula)
                .Include(p => p.Sala)
                .Include(p => p.Tarifa1)
                .Include(p => p.Tarifa2)
                .Include(p => p.Tarifa3)
                .Include(p => p.Tarifa4)
                .Include(p => p.Tarifa5)
                .Include(p => p.Tarifa6)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programar == null)
            {
                return NotFound();
            }

            return View(programar);
        }

        // GET: Programar/Create
        public IActionResult Create()
        {
             

            var salas = _context.Salas
                .Include(p => p.Tipo)
                .Select(x => new
                {
                    x.Id,
                    DescSalaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
                });

            var peliculas = _context.Peliculas
               .Include(p => p.Tipo)
               .Select(x => new
               {
                   x.Id,
                   DescPeliculaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
               });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo");
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo");
            ViewData["Tarifa1RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            ViewData["Tarifa2RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            ViewData["Tarifa3RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            ViewData["Tarifa4RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            ViewData["Tarifa5RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            ViewData["Tarifa6RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion");
            return View();
        }

        // POST: Programar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,Tarifa1RefId,Tarifa2RefId,Tarifa3RefId,Tarifa4RefId,Tarifa5RefId,Tarifa6RefId,FechaRegistro")] Programar programar)
        {
             

            var coincideTipoSalaPelicula = true;
            if (programar.PeliculaRefId.HasValue && programar.SalaRefId.HasValue)
            {
                var pelicula = _context.Peliculas.Include(p => p.Tipo).Where(x => x.Id.Equals(programar.PeliculaRefId)).FirstOrDefault();
                var sala = _context.Salas.Include(p => p.Tipo).Where(x => x.Id.Equals(programar.SalaRefId)).FirstOrDefault();
                coincideTipoSalaPelicula = pelicula.Tipo.Descripcion.Equals(sala.Tipo.Descripcion);

            }
            
            if (ModelState.IsValid && coincideTipoSalaPelicula)
            {
                _context.Add(programar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }else
            {
                if(!coincideTipoSalaPelicula)
                    ModelState.AddModelError("ValidationError", "El tipo de Sala no coincide con de la Pelicula.");

            }
            var salas = _context.Salas
                .Include(p => p.Tipo)
                .Select(x => new
                {
                    x.Id,
                    DescSalaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
                });

            var peliculas = _context.Peliculas
               .Include(p => p.Tipo)
               .Select(x => new
               {
                   x.Id,
                   DescPeliculaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
               });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", programar.Tarifa1RefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", programar.Tarifa1RefId);
            ViewData["Tarifa1RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa1RefId);
            ViewData["Tarifa2RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa2RefId);
            ViewData["Tarifa3RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa3RefId);
            ViewData["Tarifa4RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa4RefId);
            ViewData["Tarifa5RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa5RefId);
            ViewData["Tarifa6RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa6RefId);
            return View(programar);
        }

        // GET: Programar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             
            if (id == null || _context.Programaciones == null)
            {
                return NotFound();
            }

            var programar = await _context.Programaciones.FindAsync(id);
            if (programar == null)
            {
                return NotFound();
            }
            var salas = _context.Salas
                .Include(p => p.Tipo)
                .Select(x => new
                {
                    x.Id,
                    DescSalaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
                });

            var peliculas = _context.Peliculas
               .Include(p => p.Tipo)
               .Select(x => new
               {
                   x.Id,
                   DescPeliculaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
               });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", programar.Tarifa1RefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", programar.Tarifa1RefId);
            ViewData["Tarifa1RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa1RefId);
            ViewData["Tarifa2RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa2RefId);
            ViewData["Tarifa3RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa3RefId);
            ViewData["Tarifa4RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa4RefId);
            ViewData["Tarifa5RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa5RefId);
            ViewData["Tarifa6RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa6RefId);
            return View(programar);
        }

        // POST: Programar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,Tarifa1RefId,Tarifa2RefId,Tarifa3RefId,Tarifa4RefId,Tarifa5RefId,Tarifa6RefId,FechaRegistro")] Programar programar)
        {
             
            if (id != programar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramarExists(programar.Id))
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
            var salas = _context.Salas
                .Include(p => p.Tipo)
                .Select(x => new
                {
                    x.Id,
                    DescSalaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
                });

            var peliculas = _context.Peliculas
               .Include(p => p.Tipo)
               .Select(x => new
               {
                   x.Id,
                   DescPeliculaTipo = x.Descripcion + " - " + x.Tipo.Descripcion
               });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", programar.Tarifa1RefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", programar.Tarifa1RefId);
            ViewData["Tarifa1RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa1RefId);
            ViewData["Tarifa2RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa2RefId);
            ViewData["Tarifa3RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa3RefId);
            ViewData["Tarifa4RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa4RefId);
            ViewData["Tarifa5RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa5RefId);
            ViewData["Tarifa6RefId"] = new SelectList(_context.Tarifas, "Id", "Descripcion", programar.Tarifa6RefId);
            return View(programar);
        }

        // GET: Programar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
             
            if (id == null || _context.Programaciones == null)
            {
                return NotFound();
            }

            var programar = await _context.Programaciones
                .Include(p => p.Pelicula)
                .Include(p => p.Sala)
                .Include(p => p.Tarifa1)
                .Include(p => p.Tarifa2)
                .Include(p => p.Tarifa3)
                .Include(p => p.Tarifa4)
                .Include(p => p.Tarifa5)
                .Include(p => p.Tarifa6)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programar == null)
            {
                return NotFound();
            }

            return View(programar);
        }

        // POST: Programar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             
            if (_context.Programaciones == null)
            {
                return Problem("Entity set 'CineUTNContext.Programaciones'  is null.");
            }
            var programar = await _context.Programaciones.FindAsync(id);
            if (programar != null)
            {
                _context.Programaciones.Remove(programar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramarExists(int id)
        {
          return (_context.Programaciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
