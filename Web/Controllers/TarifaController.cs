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
    public class TarifaController : Controller
    {
        private readonly CineUTNContext _context;

        public TarifaController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Tarifa
        public async Task<IActionResult> Index()
        {
             
            var cineUTNContext = _context.Tarifas.Include(t => t.ListaPrecio);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: Tarifa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             
            if (id == null || _context.Tarifas == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas
                .Include(t => t.ListaPrecio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // GET: Tarifa/Create
        public IActionResult Create()
        {
             
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion");
            return View();
        }

        // POST: Tarifa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,PorcentajeDescuento,ListaPrecioRefId,FechaRegistro")] Tarifa tarifa)
        {
             
            if (ModelState.IsValid)
            {
                if (tarifa.ListaPrecioRefId != null && tarifa.ListaPrecioRefId != 0)
                {
                    var listaPrecio = await _context.ListaPrecios.FindAsync(tarifa.ListaPrecioRefId);
                    tarifa.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * tarifa.PorcentajeDescuento / 100);
                }
                else
                    tarifa.TarifaPrecio = 0;

                _context.Add(tarifa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // GET: Tarifa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             
            if (id == null || _context.Tarifas == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
            {
                return NotFound();
            }
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // POST: Tarifa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,PorcentajeDescuento,ListaPrecioRefId,FechaRegistro")] Tarifa tarifa)
        {
             
            if (id != tarifa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (tarifa.ListaPrecioRefId != null && tarifa.ListaPrecioRefId != 0)
                    {
                        var listaPrecio = await _context.ListaPrecios.FindAsync(tarifa.ListaPrecioRefId);
                        tarifa.TarifaPrecio = listaPrecio.Precio - (listaPrecio.Precio * tarifa.PorcentajeDescuento / 100);
                    }
                    else
                        tarifa.TarifaPrecio = 0;

                    _context.Update(tarifa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarifaExists(tarifa.Id))
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
            ViewData["ListaPrecioRefId"] = new SelectList(_context.ListaPrecios, "Id", "Descripcion", tarifa.ListaPrecioRefId);
            return View(tarifa);
        }

        // GET: Tarifa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
             
            if (id == null || _context.Tarifas == null)
            {
                return NotFound();
            }

            var tarifa = await _context.Tarifas
                .Include(t => t.ListaPrecio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return View(tarifa);
        }

        // POST: Tarifa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             
            if (_context.Tarifas == null)
            {
                return Problem("Entity set 'CineUTNContext.Tarifas'  is null.");
            }
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa != null)
            {
                _context.Tarifas.Remove(tarifa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TarifaExists(int id)
        {
          return (_context.Tarifas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
