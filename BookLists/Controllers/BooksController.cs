using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookLists.DB;
using BookLists.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLists.Controllers
{
    public class BooksController : Controller
    {
        private readonly DBContext _db;

        public BooksController(DBContext _db)
        {
            this._db = _db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book b)
        {
            if (!ModelState.IsValid)
            {
                return View(b);
            }

            b.ID = Guid.NewGuid().ToString();
            this._db.Books.Add(b);

            await this._db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book b = await this._db.Books.SingleOrDefaultAsync(m => m.ID == id);
            if (b == null)
            {
                return NotFound();
            }

            return View(b);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book b = await this._db.Books.SingleOrDefaultAsync(m => m.ID == id);
            if (b == null)
            {
                return NotFound();
            }

            return View(b);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book b)
        {
            if (!ModelState.IsValid)
            {
                return View(b);
            }

            if (b == null)
            {
                return BadRequest();
            }

            if (b.ID == null)
            {
                return NotFound();
            }

            this._db.Update(b).State = EntityState.Modified;
            this._db.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = b.ID });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book b = await this._db.Books.SingleOrDefaultAsync(m => m.ID == id);
            if (b == null)
            {
                return NotFound();
            }

            return View(b);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book target = await this._db.Books.SingleOrDefaultAsync(m => m.ID == id);
            if (target == null)
            {
                return NotFound();
            }

            this._db.Remove(target);

            await this._db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}