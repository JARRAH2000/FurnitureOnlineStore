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
    public class CommentsController : Controller
    {
        private readonly ModelContext _context;

        public CommentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Comments.Include(c => c.Furniture).Include(c => c.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Furniture)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Publish,UserId,FurnitureId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", comment.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", comment.UserId);
            return View(comment);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WriteComment(string text)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Authentication");
            if (text == null) RedirectToAction("ShowProduct", "Furnitures", new { id = (decimal)HttpContext.Session.GetInt32("FurnitureId") });
            Comment comment = new Comment()
            {
                Description = text,
                FurnitureId = (decimal)HttpContext.Session.GetInt32("FurnitureId"),
                UserId = (decimal)userId,
            };
            _context.Add(comment);
            _context.SaveChanges();
            return RedirectToAction("ShowProduct", "Furnitures", new { id = comment.FurnitureId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteComment(decimal?id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null)
            {
                _context.Remove(comment);
                _context.SaveChanges();
            }
            return RedirectToAction("ShowComments", "Activites");
        }



        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", comment.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Description,Publish,UserId,FurnitureId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", comment.FurnitureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Firstname", comment.UserId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Furniture)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(decimal id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
