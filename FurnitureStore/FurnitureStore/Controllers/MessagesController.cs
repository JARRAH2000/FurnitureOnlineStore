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
    public class MessagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MessagesController(ModelContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.OrderByDescending(msg => msg.SendTime).ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Subject,Text,ImagePath,SendTime,Email,ImageFile")] Message message)
        {
			if (ModelState.IsValid)
            {
                message.ImagePath = await UplaodImage(message.ImageFile);
                message.SendTime = DateTime.Now;
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateMessage(string email,string subject,string messageText,IFormFile image)
		{
            if (ModelState.IsValid)
            {
                Message message = new Message()
                {
                    Email = email,
                    Subject = subject,
                    Text = messageText,
                    SendTime = DateTime.Now,
                    ImagePath = await UplaodImage(image)
                };
				_context.Add(message);
				await _context.SaveChangesAsync();
			}
            return RedirectToAction("Index", "Home");
		}
		public async Task<string> UplaodImage(IFormFile ImageFile)
		{
            if (ImageFile == null) return "defaultMessageImage.jpeg";
			string wwwRootPath = _hostEnvironment.WebRootPath;
			string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
			string path = Path.Combine(wwwRootPath + "/Images/", fileName);
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				await ImageFile.CopyToAsync(fileStream);
			}
			return fileName;
		}

		// GET: Messages/Edit/5
		public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Subject,Text,ImagePath,SendTime,Email")] Message message)
        {
            if (id != message.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.Id))
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
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var message = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(decimal id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
