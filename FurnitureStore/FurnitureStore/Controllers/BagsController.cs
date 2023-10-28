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
    public class BagsController : Controller
    {
        private readonly ModelContext _context;

        public BagsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Bags
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Bags.Include(b => b.Furniture).Include(b => b.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Bags/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Furniture)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // GET: Bags/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: Bags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FurnitureId,Quantity")] Bag bag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", bag.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", bag.UserId);
            return View(bag);
        }

        // GET: Bags/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags.FindAsync(id);
            if (bag == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", bag.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", bag.UserId);
            return View(bag);
        }

        // POST: Bags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,UserId,FurnitureId,Quantity")] Bag bag)
        {
            if (id != bag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BagExists(bag.Id))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name", bag.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", bag.UserId);
            return View(bag);
        }

        // GET: Bags/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bag = await _context.Bags
                .Include(b => b.Furniture)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bag == null)
            {
                return NotFound();
            }

            return View(bag);
        }

        // POST: Bags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var bag = await _context.Bags.FindAsync(id);
            _context.Bags.Remove(bag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BagExists(decimal id)
        {
            return _context.Bags.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(decimal? furId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authentication");
            if (furId == null) return NotFound();//modify : return Index Home 
            decimal userId = (decimal)HttpContext.Session.GetInt32("UserID");
            Bag bag = _context.Bags.Where(bag => bag.FurnitureId == furId && bag.UserId == userId).FirstOrDefault();
            if (bag != null)
            {
                bag.Quantity++;
                _context.Update(bag);
                _context.SaveChanges();
            }
            else
            {
                bag = new Bag();
                bag.UserId = userId;
                bag.FurnitureId = (decimal)furId;
                bag.Quantity = 1;
                try
                {
                    _context.Add(bag);
                    _context.SaveChanges();
                }
                catch(Exception)
                {

                }
            }
            
            return RedirectToAction("ShowProduct", "Furnitures", new { id = furId });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  DropFromCart(decimal?furId)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authentication");
            if (furId == null) return NotFound();//modify : return Index Home 
            decimal userId = (decimal)HttpContext.Session.GetInt32("UserID");
            var product = _context.Bags.Where(bag => bag.FurnitureId == furId && bag.UserId == userId).FirstOrDefault();
            if (product != null)
            {
                _context.Remove(product);
                _context.SaveChangesAsync();
            }
            return RedirectToAction("ShowBag", "Activites");
        }
    }
}
