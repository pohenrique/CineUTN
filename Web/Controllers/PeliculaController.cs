using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;
using Web.ViewModels;

namespace Web.Controllers
{
    public class PeliculaController : Controller
    {
        private readonly CineUTNContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PeliculaController(CineUTNContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;

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
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulos, "Id", "Descripcion");
            return View();
        }

        // POST: Pelicula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeliculaViewModel model)
        {
            ViewBag.SignIn = true;
            string uniqueFileName = UploadedFile(model);
            if (ModelState.IsValid)
            {
                Pelicula pelicula = new Pelicula()
                {
                    ImagemPelicula = uniqueFileName,
                    Clasificacion = model.Clasificacion,
                    Descripcion = model.Descripcion,    
                    Duracion = model.Duracion,  
                    FechaEstreno = model.FechaEstreno,  
                    FechaRegistro = model.FechaRegistro,
                    GeneroRefId = model.GeneroRefId,
                    TipoRefId = model.TipoRefId,
                    SubtituloRefId = model.SubtituloRefId,
                    
                };
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", model.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", model.TipoRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulos, "Id", "Descripcion");
            return View(model);
        }

        private string UploadedFile(PeliculaViewModel model)
        {
            string uniqueFileName = null;

            if (model.Imagem != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Imagem.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Imagem.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
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

            PeliculaViewModel peliculaViewModel = new PeliculaViewModel()
            {
                
                Clasificacion = pelicula.Clasificacion,
                Descripcion = pelicula.Descripcion,
                Duracion = pelicula.Duracion,
                FechaEstreno = pelicula.FechaEstreno,
                FechaRegistro = pelicula.FechaRegistro,
                GeneroRefId = pelicula.GeneroRefId,
                TipoRefId = pelicula.TipoRefId,
                SubtituloRefId = pelicula.SubtituloRefId,

            };

            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", pelicula.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", pelicula.TipoRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulos, "Id", "Descripcion");
            return View(peliculaViewModel);
        }

        // POST: Pelicula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeliculaViewModel model)
        {
            ViewBag.SignIn = true;
            string uniqueFileName = UploadedFile(model);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pelicula = await _context.Peliculas.FindAsync(id);


                    pelicula.ImagemPelicula = uniqueFileName;
                    pelicula.Clasificacion = model.Clasificacion;
                    pelicula.Descripcion = model.Descripcion;
                    pelicula.Duracion = model.Duracion;
                    pelicula.FechaEstreno = model.FechaEstreno;
                    pelicula.FechaRegistro = model.FechaRegistro;
                    pelicula.GeneroRefId = model.GeneroRefId;
                    pelicula.TipoRefId = model.TipoRefId;
                    pelicula.SubtituloRefId = model.SubtituloRefId;

                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(model.Id))
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
            ViewData["GeneroRefId"] = new SelectList(_context.Generos, "Id", "Descripcion", model.GeneroRefId);
            ViewData["TipoRefId"] = new SelectList(_context.Tipos, "Id", "Descripcion", model.TipoRefId);
            ViewData["SubtituloRefId"] = new SelectList(_context.Subtitulos, "Id", "Descripcion");
            return View(model);
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
