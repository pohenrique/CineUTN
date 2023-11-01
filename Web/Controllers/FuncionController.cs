using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Web.Models;
using Web.Repos;

namespace Web.Controllers
{
    public class FuncionController : Controller
    {
        private readonly CineUTNContext _context;

        public FuncionController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Funcion
        public async Task<IActionResult> Index()
        {
            var cineUTNContext = _context.Funciones
                                    .Include(f => f.Tarifas)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Funcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // GET: Funcion/Create
        public IActionResult Create()
        {

            var model = new Funcion();
            model.Tarifas.Add(new FuncionTarifa());

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

            var tarifas = _context.Tarifas
               .Include(p => p.ListaPrecio)
               .Select(x => new
               {
                   x.Id,
                   DescTarifaPrecio = x.Descripcion + " - " + x.ListaPrecio.Precio
               });

            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");
            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo");
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo");

            return View(model);
        }

        // POST: Funcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,Tarifas")] Funcion funcion)
        {
            var coincideTipoSalaPelicula = true;
            if (funcion.PeliculaRefId.HasValue && funcion.SalaRefId.HasValue)
            {
                var pelicula = _context.Peliculas.Include(p => p.Tipo).Where(x => x.Id.Equals(funcion.PeliculaRefId)).FirstOrDefault();
                var sala = _context.Salas.Include(p => p.Tipo).Where(x => x.Id.Equals(funcion.SalaRefId)).FirstOrDefault();
                coincideTipoSalaPelicula = pelicula.Tipo.Descripcion.Equals(sala.Tipo.Descripcion);

            }

            if (ModelState.IsValid && coincideTipoSalaPelicula)
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if(!coincideTipoSalaPelicula)
                    ModelState.AddModelError("ValidationError", "El tipo de Sala no coincide con lo de la Pelicula.");

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

            var tarifas = _context.Tarifas
              .Include(p => p.ListaPrecio)
              .Select(x => new
              {
                  x.Id,
                  DescTarifaPrecio = x.Descripcion + " - " + x.ListaPrecio.Precio
              });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", funcion.SalaRefId);
            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");

            return View(funcion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddFuncionTarifa([Bind("Tarifas")] Funcion funcion)
        {
            funcion.Tarifas.Add(new FuncionTarifa());
            var tarifas = _context.Tarifas
               .Include(p => p.ListaPrecio)
               .Select(x => new
               {
                   x.Id,
                   DescTarifaPrecio = x.Descripcion + " - " + x.ListaPrecio.Precio
               });

            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");

            return PartialView("FuncionTarifa", funcion);
        }

        // GET: Funcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones.FindAsync(id);
            if (funcion == null)
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

            var tarifas = _context.Tarifas
              .Include(p => p.ListaPrecio)
              .Select(x => new
              {
                  x.Id,
                  DescTarifaPrecio = x.Descripcion + " - " + x.ListaPrecio.Precio
              });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", funcion.SalaRefId);
            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");

            return View(funcion);
        }

        // POST: Funcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,FechaRegistro")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionExists(funcion.Id))
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

            var tarifas = _context.Tarifas
              .Include(p => p.ListaPrecio)
              .Select(x => new
              {
                  x.Id,
                  DescTarifaPrecio = x.Descripcion + " - " + x.ListaPrecio.Precio
              });

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", funcion.PeliculaRefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", funcion.SalaRefId);
            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");

            return View(funcion);
        }

        // GET: Funcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funciones == null)
            {
                return NotFound();
            }

            var funcion = await _context.Funciones
                .Include(f => f.Tarifas)
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (funcion == null)
            {
                return NotFound();
            }

            return View(funcion);
        }

        // POST: Funcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funciones == null)
            {
                return Problem("Entity set 'CineUTNContext.Funciones'  is null.");
            }

            var funcion = await _context.Funciones
                .Include(t => t.Tarifas)
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (funcion != null)
            {
                _context.FuncionTarifas.RemoveRange(funcion.Tarifas);
                _context.Funciones.Remove(funcion);
                
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionExists(int id)
        {
          return (_context.Funciones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
