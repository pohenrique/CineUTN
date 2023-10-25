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
    public class PedidoItemController : Controller
    {
        private readonly CineUTNContext _context;

        public PedidoItemController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: PedidoItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.PedidoItens.ToListAsync());
        }

        // GET: PedidoItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PedidoItem = await _context.PedidoItens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PedidoItem == null)
            {
                return NotFound();
            }

            return View(PedidoItem);
        }

        // GET: PedidoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PedidoItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Quantity")] PedidoItem PedidoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(PedidoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(PedidoItem);
        }

        // GET: PedidoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PedidoItem = await _context.PedidoItens.FindAsync(id);
            if (PedidoItem == null)
            {
                return NotFound();
            }
            return View(PedidoItem);
        }

        // POST: PedidoItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Quantity")] PedidoItem PedidoItem)
        {
            if (id != PedidoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(PedidoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoItemExists(PedidoItem.Id))
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
            return View(PedidoItem);
        }

        // GET: PedidoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var PedidoItem = await _context.PedidoItens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (PedidoItem == null)
            {
                return NotFound();
            }

            return View(PedidoItem);
        }

        // POST: PedidoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var PedidoItem = await _context.PedidoItens.FindAsync(id);
            _context.PedidoItens.Remove(PedidoItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoItemExists(int id)
        {
            return _context.PedidoItens.Any(e => e.Id == id);
        }
    }
}
