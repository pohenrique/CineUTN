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
    public class PedidoController : Controller
    {
        private readonly CineUTNContext _context;

        public PedidoController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: Pedido
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pedidos.Include(t => t.Items).ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pedido = await _context.Pedidos.Include(t => t.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Pedido == null)
            {
                return NotFound();
            }

            return View(Pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            var model = new Pedido();
            model.Items.Add(new PedidoItem());
            return View(model);
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsUrgent,Items")] Pedido Pedido)
        {
            if (ModelState.IsValid)
            {
                Pedido.Created = DateTime.UtcNow;
                _context.Add(Pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPedidoItem([Bind("Items")] Pedido Pedido)
        {
            Pedido.Items.Add(new PedidoItem());
            return PartialView("PedidoItem", Pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pedido = await _context.Pedidos.FindAsync(id);
            if (Pedido == null)
            {
                return NotFound();
            }
            return View(Pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsUrgent,Created")] Pedido Pedido)
        {
            if (id != Pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(Pedido.Id))
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
            return View(Pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Pedido = await _context.Pedidos.Include(t => t.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Pedido == null)
            {
                return NotFound();
            }

            return View(Pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Pedido = await _context.Pedidos.Include(t => t.Items).FirstOrDefaultAsync(t => t.Id == id);
            _context.Pedidos.Remove(Pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
