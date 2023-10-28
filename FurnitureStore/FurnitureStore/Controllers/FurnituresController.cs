using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace FurnitureStore.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public FurnituresController(ModelContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Furnitures
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Furnitures.Include(f => f.Section);
            return View(await modelContext.ToListAsync());
        }

        // GET: Furnitures/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures
                .Include(f => f.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furniture == null)
            {
                return NotFound();
            }
            return View(furniture);
        }
        
        // GET: Furnitures/Create
        public IActionResult Create()
        {
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name");
            return View();
        }

        // POST: Furnitures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Price,Description,Imagepath,SectionId,Cost,ImageFile")] Furniture furniture)
        {
            if (ModelState.IsValid)
            {
                if (furniture.Name != null && furniture.Description != null && furniture.Cost != null && furniture.ImageFile != null && furniture.Quantity != null)
                {
                    furniture.Imagepath = await UploadImage(furniture.ImageFile);
                    _context.Add(furniture);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name",furniture.SectionId);
            return View(furniture);
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
        // GET: Furnitures/Edit/5
        public async Task<IActionResult>ShowImages(int id)
        {
            ViewBag.FurName = await _context.Furnitures.Where(fur => fur.Id == id).FirstOrDefaultAsync();
            var images = await _context.Images.Where(img => img.FurnitureId == id).Include(img => img.Furniture).ToListAsync();
            return View(images);
        }

        public IActionResult AddImage(decimal furId)
        {
            TempData["FurId"] = furId;
            return View();
        }
        //{
        [HttpPost]
        public async Task<IActionResult> AddImage(decimal furId, IFormFile file)
        {
            if (file == null) return RedirectToAction("ShowImage", "Furnitures", new { furId });
            Image image = new Image();
            image.FurnitureId = furId;
            image.Imagepath = await UploadImage(file);
            await _context.AddAsync(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowImages", "Furnitures", new { id=furId });
        }
        public async Task<IActionResult> DeleteImage(decimal id, decimal furId)
        {
            Image image = await _context.Images.FindAsync(id);
            _context.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowImages", "Furnitures", new { id = furId });
        }



        public async Task<IActionResult>ShowOffers(decimal id)
        {
            ViewBag.FurName = await _context.Furnitures.Where(fur => fur.Id == id).FirstOrDefaultAsync();
            return View(await _context.Offers.Where(ofr => ofr.FurnitureId == id).OrderByDescending(ofr => ofr.EndDate).ToListAsync());
        }

        public IActionResult AddOffer(decimal furId)
        {
            TempData["FurId"] = furId;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOffer(decimal furId, decimal percentage, DateTime startDate, DateTime endDate, string description)
        {
            if (description == null || startDate == null || endDate == null || endDate < startDate || endDate < DateTime.Today || percentage > 100 || percentage < 0) return RedirectToAction("AddOffer", "Furnitures", new { furId });
            Offer offer = new Offer();
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            offer.FurnitureId = furId;
            offer.StartDate = startDate;
            offer.EndDate = endDate;
            offer.Description = description;
            offer.Percentage = percentage;

            var offers = _context.Offers.Where(ofr => ofr.FurnitureId == furId).ToList();
            foreach (Offer ofr in offers)
            {
                ofr.EndDate = DateTime.Today.AddDays(-1);
                _context.Update(ofr);
            }

            await _context.AddAsync(offer);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowOffers", "Furnitures", new { id = furId });
        }
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures.FindAsync(id);
            if (furniture == null)
            {
                return NotFound();
            }

            TempData["FURIMAGE"] = furniture.Imagepath;
            TempData["FURNAME"] = furniture.Name;
            TempData["FURDESC"] = furniture.Description;

            TempData["COST"] = furniture.Cost.ToString();
            TempData["PRICE"] = furniture.Price.ToString();
            TempData["QUANTITY"] = furniture.Quantity.ToString();

            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name",furniture.SectionId);//SectionId
            return View(furniture);
        }

        // POST: Furnitures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Quantity,Price,Description,Imagepath,SectionId,Cost,ImageFile")] Furniture furniture)
        {
            if (id != furniture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (furniture.ImageFile != null)
                        furniture.Imagepath = await UploadImage(furniture.ImageFile);
                    else furniture.Imagepath = TempData["FURIMAGE"].ToString();
                    furniture.Description ??= TempData["FURDESC"].ToString();
                    furniture.Name ??= TempData["FURNAME"].ToString();

                    if (furniture.Cost <= 0||furniture.Cost==null) furniture.Cost = Convert.ToDecimal(TempData["COST"].ToString());
                    if (furniture.Price.ToString() ==string.Empty) furniture.Price = Convert.ToDecimal(TempData["PRICE"].ToString());
                    if (furniture.Quantity <= 0||furniture.Quantity==null) furniture.Quantity = Convert.ToDecimal(TempData["QUANTITY"].ToString());

                    _context.Update(furniture);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurnitureExists(furniture.Id))
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
            
            ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "SectionName", furniture.SectionId);
            return View(furniture);
        }

        // GET: Furnitures/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var furniture = await _context.Furnitures
                .Include(f => f.Section)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (furniture == null)
            {
                return NotFound();
            }

            return View(furniture);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var furniture = await _context.Furnitures.FindAsync(id);
            _context.Furnitures.Remove(furniture);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurnitureExists(decimal id)
        {
            return _context.Furnitures.Any(e => e.Id == id);
        }


        public async Task<IActionResult> ShowProduct(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var images = await _context.Images.Where(image => image.FurnitureId == id).ToListAsync();
            var comments = await _context.Comments.Where(comment => comment.FurnitureId == id && comment.Publish == "Y").Include(name => name.User).ToListAsync();
            var ratings = await _context.Ratings.Where(rating => rating.FurnitureId == id).Include(name => name.User).ToListAsync();
            var offers = await _context.Offers.Where(offer => offer.FurnitureId == id && offer.StartDate<=DateTime.Today&& offer.EndDate >= DateTime.Today).OrderByDescending(offer => offer.Id).Take(1).ToListAsync();
            
            var furniture = await _context.Furnitures.Include(p => p.Section).FirstOrDefaultAsync(product => product.Id == id);
            
            HttpContext.Session.SetInt32("FurnitureId", (int)id);
            ViewBag.IsSession = (HttpContext.Session.GetInt32("UserID") == null ? -1 : HttpContext.Session.GetInt32("UserID"));
            
            int useridentity = ViewBag.IsSession;
            
            ViewBag.Favourite = await _context.Favourites.Where(fav => fav.FurnitureId == id && fav.UserId == useridentity).FirstOrDefaultAsync();
            ViewBag.Rates = await _context.Ratings.Where(rate => rate.FurnitureId == id && rate.UserId == useridentity).FirstOrDefaultAsync();
                        
            var tuple = Tuple.Create<Furniture, IEnumerable<Image>, IEnumerable<Comment>, IEnumerable<Rating>, Offer>(furniture, images, comments, ratings, (offers.Count == 0 ? null : offers[0]));
            return View(tuple);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
			if (name == null) return View(await _context.Furnitures.Include(f => f.Section).ToListAsync());
			return View(await _context.Furnitures.Where(f => f.Name.ToLower().Contains(name.ToLower())).Include(f => f.Section).ToListAsync());
        }

		[HttpGet]
		public async Task<IActionResult> FurnituresOfSection(decimal? id)
		{
            if (id == null) return View(await _context.Furnitures.Include(f => f.Section).ToListAsync());
            return View(await _context.Furnitures.Where(f => f.SectionId == id).Include(f => f.Section).ToListAsync());
		}
		
        public async Task<IActionResult> ShowComments(decimal id)
        {
            ViewBag.FurName = await _context.Furnitures.Where(fur => fur.Id == id).FirstOrDefaultAsync();
            return View(await _context.Comments.Where(c => c.FurnitureId == id).Include(u=>u.User).ToListAsync());
        }

        public IActionResult PublishOrHideComment(decimal? id, bool publish)
        {
            Comment comment = _context.Comments.Find(id);
            comment.Publish = publish ? "Y" : "N";
            _context.Update(comment);
            _context.SaveChangesAsync();
            return RedirectToAction("ShowComments", "Furnitures", new { id = comment.FurnitureId });
        }
    }
}
