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
    public class TestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public TestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Testimonials.Include(t => t.Sender);
            return View(await modelContext.ToListAsync());
        }



        public async Task<IActionResult>ListAll()
        {
            return View(await _context.Testimonials.Where(t => t.Publish == "Y").Include(u=>u.Sender).ToListAsync());
        }
        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        public IActionResult Create()
        {
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Firstname");
            return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,SendTime,Publish,SenderId")] Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Firstname", testimonial.SenderId);
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Firstname", testimonial.SenderId);
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Text,SendTime,Publish,SenderId")] Testimonial testimonial)
        {
            if (id != testimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.Id))
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
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Firstname", testimonial.SenderId);
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonials
                .Include(t => t.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            _context.Testimonials.Remove(testimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(decimal id)
        {
            return _context.Testimonials.Any(e => e.Id == id);
        }


        
        public IActionResult WriteTestimonial()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            if (id == null) return RedirectToAction("Login", "Authentication");
            User user = _context.Users.FirstOrDefault(cus => cus.Id == id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult WriteTestimonial(string testimonialText)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            if (id == null) return RedirectToAction("Login", "Authentication");
            if (testimonialText == null) return View();
            Testimonial testimonial = new Testimonial()
            {
                Text = testimonialText,
                SenderId = (decimal)id,
                SendTime = DateTime.Now,
            };
            _context.Add(testimonial);
            _context.SaveChanges();
            return View(_context.Users.FirstOrDefault(user => user.Id == id));
        }
        public IActionResult PublishOrHide(decimal? id,bool publish)
        {
            Testimonial testimonial = _context.Testimonials.Find(id);
            testimonial.Publish = publish ? "Y" : "N";
            _context.Update(testimonial);
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
