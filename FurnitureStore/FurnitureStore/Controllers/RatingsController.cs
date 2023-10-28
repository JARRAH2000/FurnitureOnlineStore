using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Http;

namespace FurnitureStore.Controllers
{
    public class RatingsController : Controller
    {
        private readonly ModelContext _context;

        public RatingsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Ratings.Include(r => r.Furniture).Include(r => r.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Furniture)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stars,UserId,FurnitureId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", rating.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", rating.UserId);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", rating.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", rating.UserId);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Stars,UserId,FurnitureId")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", rating.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", rating.UserId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Ratings
                .Include(r => r.Furniture)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(decimal id)
        {
            return _context.Ratings.Any(e => e.Id == id);
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRate(decimal rate)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authentication");
            else if (HttpContext.Session.GetInt32("FurnitureId") == null) return NotFound();
            decimal usrId = (decimal)HttpContext.Session.GetInt32("UserID");
            decimal furId = (decimal)HttpContext.Session.GetInt32("FurnitureId");

            Rating rating = new Rating {
                Stars = rate,
                FurnitureId = furId,
                UserId = usrId,
            };
            try
            {
                _context.Add(rating);
                _context.SaveChanges();
            }
            catch(Exception)
            {

            }
            
            return RedirectToAction("ShowProduct", "Furnitures", new { id = furId });

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRate()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authenticaton");
            else if (HttpContext.Session.GetInt32("FurnitureId") == null) return NotFound();
            decimal usrId = (decimal)HttpContext.Session.GetInt32("UserID");
            decimal furId = (decimal)HttpContext.Session.GetInt32("FurnitureId");

            Rating rating = _context.Ratings.Where(rat => rat.FurnitureId == furId && rat.UserId == usrId).FirstOrDefault();
            if (rating != null)
            {
                _context.Remove(rating);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ShowProduct", "Furnitures", new { id = furId });
        }
    }
}
