using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;

namespace FurnitureStore.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public SectionsController(ModelContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Sections
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sections.ToListAsync());
        }

        // GET: Sections/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,ImageFile")] Section section)
        {
            if (ModelState.IsValid)
            {
                if (section.ImageFile != null && section.Name != null)
                {
                    section.ImagePath = await UploadImage(section.ImageFile);
                    _context.Add(section);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(section);
        }
        public async Task<string> UploadImage(IFormFile ImageFile)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return fileName;
        }
        // GET: Sections/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            TempData["SectionName"] = section.Name;
            TempData["SectionImage"] = section.ImagePath;

            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,ImagePath,ImageFile")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (section.ImageFile != null) section.ImagePath = await ModifyImage(section.ImageFile);
                    else section.ImagePath = TempData["SectionImage"].ToString();
                    section.Name ??= section.Name = TempData["SectionName"].ToString();
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
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
            return View(section);
        }
        public async Task<string> ModifyImage(IFormFile ImageFile)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return fileName;
        }
        // GET: Sections/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Sections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var section = await _context.Sections.FindAsync(id);
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionExists(decimal id)
        {
            return _context.Sections.Any(e => e.Id == id);
        }


		[HttpGet]
		public async Task<IActionResult> Search(string name)
		{
			if (name == null) return View(await _context.Sections.Include(s=>s.Furnitures).ToListAsync());
			return View(await _context.Sections.Where(f => f.Name.ToLower().Contains(name.ToLower())).Include(s => s.Furnitures).ToListAsync());
			//return View(await _context.Furnitures.Include(f => f.Section).ToListAsync());
		}
	}
}
