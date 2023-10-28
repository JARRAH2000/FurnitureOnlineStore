using Microsoft.AspNetCore;
using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace FurnitureStore.Controllers
{
    public class ClientController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ClientController(ModelContext context,IWebHostEnvironment hostEnvironment) 
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

		public async Task<IActionResult> Index()
		{
			try
			{
				var customer = _context.Users.ToList();
				var account = _context.Accounts.ToList();

				User user = await _context.Users.Where(customer => customer.Id == HttpContext.Session.GetInt32("UserID")).FirstOrDefaultAsync();
				return View(user);
			}
			catch (Exception)
			{

			}
			return RedirectToAction("Login", "Authentication");
		}

		public async Task<IActionResult> Edit(decimal? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			//user.Imagepath ??= "defaultUserImage.png";
			TempData["USRIMAGE"] = user.Imagepath;
			TempData["FNAME"] = user.Firstname;
			TempData["LNAME"] = user.Lastname;

			TempData["EMAIL"] = _context.Accounts.Where(acc => acc.UserId == user.Id).FirstOrDefault().Username;
			TempData["PASS"] = _context.Accounts.Where(acc => acc.UserId == user.Id).FirstOrDefault().Password;
			TempData["QUANTITY"] = Convert.ToDateTime(user.Birthdate).ToString("yyyy-MM-dd");
			HttpContext.Session.Remove("FurnitureId");


			ViewBag.Role = HttpContext.Session.GetInt32("RoleID");
            //ViewData["SectionId"] = new SelectList(_context.Sections, "Id", "Name", furniture.SectionId);//SectionId
            return View(user);
		}

		public IActionResult UpdateInformation()
        {
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(decimal id, [Bind("Id,Firstname,Lastname,Birthdate,Sex,Imagepath,ImageFile")] User user,string password,string newpassword)
		{
			if (id != user.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					if (HttpContext.Session.GetInt32("UserID") == null) return RedirectToAction("Login", "Authentication");
                    //ensure all are filled or previous data
                    if (user.ImageFile != null)
						user.Imagepath = await UploadImage(user.ImageFile);
					else user.Imagepath = TempData["USRIMAGE"].ToString();
					user.Sex = user.Sex[0].ToString();
					_context.Update(user);

					if (password != null && newpassword != null)
					{
						Account account = await _context.Accounts.Where(acc => acc.UserId == id).FirstOrDefaultAsync();
						if (password != account.Password) 
						{ 
							TempData["PasswordState"] = false;

						}
						else
						{
							TempData["PasswordState"] = true;
                            account.Password = newpassword;
							_context.Update(account);
						}
					}
					

					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!UserExists(user.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Edit));
			}
			return View(user);
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
		private bool UserExists(decimal id)
		{
			return _context.Users.Any(e => e.Id == id);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditBag(decimal id,decimal amount)
		{
			Bag bag = _context.Bags.Where(b => b.Id == id).FirstOrDefault();
			try
			{
				bag.Quantity = amount;
				_context.Update(bag);
				_context.SaveChanges();
			}
			catch(Exception)
			{

			}
			return RedirectToAction("ShowBag", "Activites");
		}
	}
}
