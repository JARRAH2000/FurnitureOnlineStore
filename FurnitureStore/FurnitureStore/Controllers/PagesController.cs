using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FurnitureStore.Controllers
{
    public class PagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PagesController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Pages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pages.ToListAsync());
        }

        // GET: Pages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }

        // GET: Pages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,About,AboutImagePath,AboutImage,MainLogoPath,MainLogo,SecLogoPath,TopImagePath,TopImage,Greeting")] Pages pages)
        {
            if (ModelState.IsValid)
            {
                if (pages.AboutImage != null && pages.TopImage != null && pages.MainLogo != null &&pages.About!=null&&pages.Greeting!=null)
                {
                    pages.AboutImagePath = await UploadImage(pages.AboutImage);
                    pages.TopImagePath = await UploadImage(pages.TopImage);
                    pages.MainLogoPath = await UploadImage(pages.MainLogo);
                    //pages.SecLogoPath = await UploadImage(pages.SecondLogo);

                    _context.Add(pages);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pages);
        }

        private async Task<string> UploadImage(IFormFile ImageFile)
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
        // GET: Pages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages.FindAsync(id);
            if (pages == null)
            {
                return NotFound();
            }
            TempData["AboutImage"] = pages.AboutImagePath;
            TempData["TopImage"] = pages.TopImagePath;
            TempData["MainLogo"] = pages.MainLogoPath;
            TempData["SecLogo"] = pages.SecLogoPath;
            TempData["AboutText"] = pages.About;
            TempData["GreetingText"] = pages.Greeting;
            return View(pages);
        }

        // POST: Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,About,AboutImagePath,MainLogoPath,SecLogoPath,TopImagePath,Greeting,AboutImage,MainLogo,SecondLogo,TopImage")] Pages pages)
        {
            if (id != pages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (pages.AboutImage != null) { pages.AboutImagePath = await ModifyImage(pages.AboutImage); }
                    else pages.AboutImagePath = TempData["AboutImage"].ToString();
                    if (pages.TopImage != null) { pages.TopImagePath = await ModifyImage(pages.TopImage); }
                    else pages.TopImagePath = TempData["TopImage"].ToString();
                    if (pages.MainLogo != null) { pages.MainLogoPath = await ModifyImage(pages.MainLogo); }
                    else pages.MainLogoPath = TempData["MainLogo"].ToString();
                    //if (pages.SecondLogo != null) { pages.SecLogoPath = await ModifyImage(pages.SecondLogo); }
                    //else pages.SecLogoPath = TempData["SecLogo"].ToString();
                    pages.SecLogoPath ??= TempData["SecLogo"].ToString();
                    pages.Greeting ??= TempData["GreetingText"].ToString();
                    pages.About ??= pages.About = TempData["AboutText"].ToString();

                    _context.Update(pages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesExists(pages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Pages", new { id });
            }
            return View(pages);
        }

        private async Task<string> ModifyImage(IFormFile ImageFile)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return fileName;
        }
        // GET: Pages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }

        // POST: Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var pages = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(pages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesExists(decimal id)
        {
            return _context.Pages.Any(e => e.Id == id);
        }
    }
}
