using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Repos;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    public class ListaPrecioController : Controller
    {
        private readonly CineUTNContext _context;

        public ListaPrecioController(CineUTNContext context)
        {
            _context = context;
        }

        // GET: ListaPrecio
        public async Task<IActionResult> Index()
        {
             
            var cineUTNContext = _context.ListaPrecios.Include(l => l.CondicionPago);
            return View(await cineUTNContext.ToListAsync());
        }

        // GET: ListaPrecio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecios
                .Include(l => l.CondicionPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecio == null)
            {
                return NotFound();
            }

            return View(listaPrecio);
        }

        // GET: ListaPrecio/Create
        public IActionResult Create()
        {
             
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPagos, "Id", "Descripcion");
            return View();
        }

        // POST: ListaPrecio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,FechaHasta,CondicionPagoRefId,Precio,FechaRegistro")] ListaPrecio listaPrecio)
        {
             
            if (ModelState.IsValid)
            {
                _context.Add(listaPrecio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPagos, "Id", "Descripcion", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // GET: ListaPrecio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
             
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecios.FindAsync(id);
            if (listaPrecio == null)
            {
                return NotFound();
            }
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPagos, "Id", "Descripcion", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // POST: ListaPrecio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,FechaHasta,CondicionPagoRefId,Precio,FechaRegistro")] ListaPrecio listaPrecio)
        {
             
            if (id != listaPrecio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaPrecio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaPrecioExists(listaPrecio.Id))
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
            ViewData["CondicionPagoRefId"] = new SelectList(_context.CondicionPagos, "Id", "Descripcion", listaPrecio.CondicionPagoRefId);
            return View(listaPrecio);
        }

        // GET: ListaPrecio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
             
            if (id == null || _context.ListaPrecios == null)
            {
                return NotFound();
            }

            var listaPrecio = await _context.ListaPrecios
                .Include(l => l.CondicionPago)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listaPrecio == null)
            {
                return NotFound();
            }

            return View(listaPrecio);
        }

        // POST: ListaPrecio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             
            if (_context.ListaPrecios == null)
            {
                return Problem("Entity set 'CineUTNContext.ListaPrecios'  is null.");
            }
            var listaPrecio = await _context.ListaPrecios.FindAsync(id);
            if (listaPrecio != null)
            {
                _context.ListaPrecios.Remove(listaPrecio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaPrecioExists(int id)
        {
          return (_context.ListaPrecios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult ImportarListaPrecio()
        {
            return View();
        }

        [HttpPost, ActionName("MostrarDatos")]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);

            int cantidadFilas = HojaExcel.LastRowNum;

            List<ListaPrecio> lista = new List<ListaPrecio>();

            for (int i = 1; i <= cantidadFilas; i++)
            {

                IRow fila = HojaExcel.GetRow(i);

                lista.Add(new ListaPrecio
                {
                    Descripcion = fila.GetCell(0).ToString(),
                    FechaHasta = DateTime.Parse(fila.GetCell(1).ToString()),
                    CondicionPagoRefId = Int16.Parse(fila.GetCell(2).ToString()),
                    Precio = Decimal.Parse(fila.GetCell(3).ToString()),
                    FechaRegistro = DateTime.Now

                });
            }

            return StatusCode(StatusCodes.Status200OK, lista);
        }

        [HttpPost, ActionName("EnviarDatos")]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {
            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);

            int cantidadFilas = HojaExcel.LastRowNum;
            List<ListaPrecio> lista = new List<ListaPrecio>();

            for (int i = 1; i <= cantidadFilas; i++)
            {

                IRow fila = HojaExcel.GetRow(i);

                lista.Add(new ListaPrecio
                {
                    Descripcion = fila.GetCell(0).ToString(),
                    FechaHasta = DateTime.Parse(fila.GetCell(1).ToString()),
                    CondicionPagoRefId = Int16.Parse(fila.GetCell(2).ToString()),
                    Precio = Decimal.Parse(fila.GetCell(3).ToString()),
                    FechaRegistro = DateTime.Now

                });
            }

            _context.BulkInsert(lista);

            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

    }
}
