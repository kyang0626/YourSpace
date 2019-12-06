using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YourSpace.Data;
using YourSpace.Models;

namespace YourSpace.Controllers
{
    public class MyGalleryController : Controller
    {
        private readonly PhotoContext _context;

        public MyGalleryController(PhotoContext context)
        {
            _context = context;
        }


        // GET: Movies
        [Authorize]
        public async Task<IActionResult> Index(string photoTitle, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from p in _context.Photo
                                            orderby p.Title
                                            select p.Title;

            var photo = from p in _context.Photo
                        select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                photo = photo.Where(s => s.Title.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(photoTitle))
            {
                photo = photo.Where(x => x.Title == photoTitle);
            }

            var photoGenreVM = new PhotoGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Photos = await photo.ToListAsync()
            };

            return View(photoGenreVM);

        }

        //GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Photo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Movies/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,URL")] Photos photo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photo);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photo.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            return View(photo);
        }

        //POST: Movies/Edit/5
         //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         //more details see http://go.microsoft.com/fwlink/?LinkId=317598.

         [HttpPost]
         [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,URL")] Photos photo)
        {
            if (id != photo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(photo.Id))
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
            return View(photo);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Photo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Photo.FindAsync(id);
            _context.Photo.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Photo.Any(e => e.Id == id);
        }
    }
}
