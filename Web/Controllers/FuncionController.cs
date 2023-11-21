using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using NPOI.SS.Formula.Functions;
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
        public async Task<IActionResult> Index(string PeliculaRefId, string SalaRefId)
        {
            int salaRefId = 0;
            int peliculaRefId = 0;

            var cineUTNContext = _context.Funciones
                                    .Include(f => f.Tarifas)
                                    .ThenInclude(fv => fv.Tarifa)
                                    .ThenInclude(fvu => fvu.ListaPrecio)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo);

            if (PeliculaRefId != null && PeliculaRefId != "")
            {
                cineUTNContext = cineUTNContext
                                    .Where(x => x.PeliculaRefId.Equals(int.Parse(PeliculaRefId)))
                                    .Include(f => f.Tarifas)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo);
                peliculaRefId = int.Parse(PeliculaRefId);
            }

            if (SalaRefId != null && SalaRefId != "")
            {
                cineUTNContext = cineUTNContext
                                    .Where(x => x.SalaRefId.Equals(int.Parse(SalaRefId)))
                                    .Include(f => f.Tarifas)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo);
                salaRefId = int.Parse(SalaRefId);
            }

            if ((SalaRefId != null && SalaRefId != "") && (PeliculaRefId != null && PeliculaRefId != ""))
            {
                cineUTNContext = cineUTNContext
                                    .Where(x => x.SalaRefId.Equals(int.Parse(SalaRefId)) && x.PeliculaRefId.Equals(int.Parse(PeliculaRefId)))
                                    .Include(f => f.Tarifas)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo);

                salaRefId = int.Parse(SalaRefId);
                peliculaRefId = int.Parse(PeliculaRefId);
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

            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo", peliculaRefId);
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo", salaRefId);

            return View(await cineUTNContext.OrderBy(o => o.FechaHoraFuncion).ToListAsync());
        }

        // GET: Funcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var model = new Funcion();
            model.Tarifas.Add(new FuncionTarifa());

            if (id == null || _context.Funciones == null)
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

            var funcion = await _context.Funciones
                .Include(f => f.Pelicula)
                .Include(f => f.Sala)
                .Include(p => p.Tarifas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcion == null)
            {
                return NotFound();
            }

            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio");
            ViewData["PeliculaRefId"] = new SelectList(peliculas, "Id", "DescPeliculaTipo");
            ViewData["SalaRefId"] = new SelectList(salas, "Id", "DescSalaTipo");

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

            var coincideHorarioSala = true;
            if (funcion.FechaHoraFuncion.HasValue && funcion.SalaRefId.HasValue)
            {
                coincideHorarioSala = _context.Funciones
                                            .Where(x => x.FechaHoraFuncion.Equals(funcion.FechaHoraFuncion) && x.SalaRefId.Equals(funcion.SalaRefId))
                                            .ToList().Count().Equals(0);
            }

            //Eliminar Tarifas duplicadas caso exista con distinct.
            var tarifasSinRepetir = funcion.Tarifas.DistinctBy(y => y.TarifaRefId).ToList();

            if (ModelState.IsValid && coincideTipoSalaPelicula && coincideHorarioSala && funcion.Tarifas.Count.Equals(tarifasSinRepetir.Count))
            {
                _context.Add(funcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (!coincideTipoSalaPelicula)
                    ModelState.AddModelError("ValidationError", "El tipo de Sala no coincide con lo de la Pelicula.");

                if (!coincideHorarioSala)
                    ModelState.AddModelError("ValidationError", "Hay una funcion programada para esta sala en esta fecha/hora.");

                if (!funcion.Tarifas.Count.Equals(tarifasSinRepetir.Count))
                    ModelState.AddModelError("ValidationError", "Hay tarifas repetidas en tu selección.");

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
        public async Task<ActionResult> AddFuncionTarifa(Funcion funcion)
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
            var funcion = _context.Funciones
               .Include(f => f.Tarifas)
                                    .Include(f => f.Pelicula)
                                    .Include(f => f.Pelicula.Tipo)
                                    .Include(f => f.Sala)
                                    .Include(f => f.Sala.Tipo)
               .Where(x => x.Id.Equals(id))
               .FirstOrDefault();

            //var funcion = await _context.Funciones.FindAsync(id);
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
            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio", funcion);

            return View(funcion);
        }

        // POST: Funcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaHoraFuncion,PeliculaRefId,SalaRefId,Tarifas")] Funcion funcion)
        {
            if (id != funcion.Id)
            {
                return NotFound();
            }

            var coincideTipoSalaPelicula = true;
            if (funcion.PeliculaRefId.HasValue && coincideTipoSalaPelicula && funcion.SalaRefId.HasValue)
            {
                var pelicula = _context.Peliculas.Include(p => p.Tipo).Where(x => x.Id.Equals(funcion.PeliculaRefId)).FirstOrDefault();
                var sala = _context.Salas.Include(p => p.Tipo).Where(x => x.Id.Equals(funcion.SalaRefId)).FirstOrDefault();
                coincideTipoSalaPelicula = pelicula.Tipo.Descripcion.Equals(sala.Tipo.Descripcion);
            }

            var coincideHorarioSala = true;
            if (funcion.FechaHoraFuncion.HasValue && funcion.SalaRefId.HasValue)
            {

                coincideHorarioSala = _context.Funciones
                                            .Where(x => x.FechaHoraFuncion.Equals(funcion.FechaHoraFuncion) && x.SalaRefId.Equals(funcion.SalaRefId) && x.Id != funcion.Id)
                                            .ToList().Count().Equals(0);
            }

            //Eliminar Tarifas duplicadas caso exista con distinct.
            var tarifasSinRepetir = funcion.Tarifas.DistinctBy(y => y.TarifaRefId).ToList();

            if (ModelState.IsValid && coincideTipoSalaPelicula && coincideHorarioSala && funcion.Tarifas.Count().Equals(tarifasSinRepetir.Count()))
            {
                try
                {
                    var funcionTarifa = _context.FuncionTarifas.Where(x => x.FuncionId.Equals(funcion.Id));
                    _context.FuncionTarifas.RemoveRange(funcionTarifa);

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
            else
            {
                if (!coincideTipoSalaPelicula)
                    ModelState.AddModelError("ValidationError", "El tipo de Sala no coincide con lo de la Pelicula.");

                if (!coincideHorarioSala)
                    ModelState.AddModelError("ValidationError", "Hay una funcion programada para esta sala en esta fecha/hora.");

                if (funcion.Tarifas != tarifasSinRepetir)
                    ModelState.AddModelError("ValidationError", "Hay tarifas repetidas en tu selección.");

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
            ViewData["TarifaRefId"] = new SelectList(tarifas, "Id", "DescTarifaPrecio", funcion);

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
