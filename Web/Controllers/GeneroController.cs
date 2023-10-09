using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Repos;
using Web.Repos.Models;

namespace Web.Controllers
{
    public class GeneroController : Controller
    {
        private readonly CineUTNContext _context;
        public GeneroController(CineUTNContext _context) {
        
            this._context = _context;
        }

        // GET: GeneroController
        public ActionResult Index()
        {
            ViewBag.SignIn = true;
            return View(_context.Generos.ToList());
        }

        // GET: GeneroController/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.SignIn = true;
            return View();
        }

        // GET: GeneroController/Create
        public ActionResult Create()
        {
            ViewBag.SignIn = true;
            return View();
        }

        // POST: GeneroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genero genero)
        {
            try
            {
                _context.Generos.Add(genero);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SignIn = true;
                return View();
            }
        }

        // GET: GeneroController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SignIn = true;
            return View();
        }

        // POST: GeneroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SignIn = true;
                return View();
            }
        }

        // GET: GeneroController/Delete/5
        public ActionResult Delete(int id)
        {
            ViewBag.SignIn = true;
            return View();
        }

        // POST: GeneroController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.SignIn = true;
                return View();
            }
        }
    }
}
