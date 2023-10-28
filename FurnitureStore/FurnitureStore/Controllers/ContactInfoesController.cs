using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;

namespace FurnitureStore.Controllers
{
    public class ContactInfoesController : Controller
    {
        private readonly ModelContext _context;

        public ContactInfoesController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ContactInfos.ToListAsync());
        }

        // GET: ContactInfoes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // GET: ContactInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Phone,Email,Facebook,Instagram,Twitter,Youtube,Fax,Address")] ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactInfo);
        }

        // GET: ContactInfoes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos.FindAsync(id);
            if (contactInfo == null)
            {
                return NotFound();
            }
            return View(contactInfo);
        }

        // POST: ContactInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Phone,Email,Facebook,Instagram,Twitter,Youtube,Fax,Address")] ContactInfo contactInfo)
        {
            if (id != contactInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactInfoExists(contactInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "ContactInfoes", new { id });
            }
            return View(contactInfo);
        }

        // GET: ContactInfoes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactInfo = await _context.ContactInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        // POST: ContactInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactInfo = await _context.ContactInfos.FindAsync(id);
            _context.ContactInfos.Remove(contactInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactInfoExists(decimal id)
        {
            return _context.ContactInfos.Any(e => e.Id == id);
        }
    }
}
