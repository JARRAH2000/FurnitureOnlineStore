using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Http;

namespace FurnitureStore.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ModelContext _context;

        public FavouritesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Favourites
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Favourites.Include(f => f.Furniture).Include(f => f.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Furniture)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // GET: Favourites/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FurnitureId")] Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(favourite);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", favourite.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", favourite.UserId);
            return View(favourite);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MakeFavourite(decimal furId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authentication");

            Favourite favourite = _context.Favourites.Where(fav => fav.FurnitureId == furId && fav.UserId == (decimal)HttpContext.Session.GetInt32("UserID")).FirstOrDefault();
            if (favourite != null) RedirectToAction("ShowProduct", "Furnitures", new { id = favourite.FurnitureId });

            favourite = new Favourite();
            favourite.FurnitureId = furId;
            favourite.UserId = (decimal)HttpContext.Session.GetInt32("UserID");
            try
            {
                _context.Add(favourite);
                _context.SaveChanges();
            }
            catch(Exception)
            {

            }

            return RedirectToAction("ShowProduct", "Furnitures", new { id = furId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public  IActionResult RemoveFavourite(decimal furId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authenticaton");
            decimal userId = (decimal)HttpContext.Session.GetInt32("UserID");
            Favourite favourite = _context.Favourites.Where(fav => fav.FurnitureId == furId && fav.UserId == userId).FirstOrDefault();
            if (favourite != null)
            {
                _context.Favourites.Remove(favourite);
                try
                {
                    _context.SaveChanges();
                }
                catch(Exception)
                {
                    if (HttpContext.Session.GetInt32("FurnitureId") != null)
                        return RedirectToAction("ShowProduct", "Furnitures", new { id = favourite.FurnitureId });
                    return RedirectToAction("ShowPreference", "Activites");
                }
            }
  
            if (HttpContext.Session.GetInt32("FurnitureId") != null)
                return RedirectToAction("ShowProduct", "Furnitures", new { id = favourite.FurnitureId });
            return RedirectToAction("ShowPreference", "Activites");
        }


        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites.FindAsync(id);
            if (favourite == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", favourite.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", favourite.UserId);
            return View(favourite);
        }

        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,UserId,FurnitureId")] Favourite favourite)
        {
            if (id != favourite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favourite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavouriteExists(favourite.Id))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", favourite.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", favourite.UserId);
            return View(favourite);
        }

        // GET: Favourites/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _context.Favourites
                .Include(f => f.Furniture)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (favourite == null)
            {
                return NotFound();
            }

            return View(favourite);
        }

        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var favourite = await _context.Favourites.FindAsync(id);
            _context.Favourites.Remove(favourite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavouriteExists(decimal id)
        {
            return _context.Favourites.Any(e => e.Id == id);
        }
    }
}
