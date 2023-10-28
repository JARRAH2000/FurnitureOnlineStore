using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FurnitureStore.Controllers
{
    public class AuthenticationController : Controller
    {
        ModelContext _context;
        IWebHostEnvironment _hostEnvironment;
        public AuthenticationController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Register()
        {
            ViewData["ErrorMessage"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Firstname,Lastname,Birthdate,Sex,Imagepath,ImageFile")] User user, string username, string password, string repeatedpassword)
        {
            if (ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = await RegisterValidation(username, password, repeatedpassword, user);
                if (ViewData["ErrorMessage"] != null) return View(user);
                user.Sex = user.Sex[0].ToString();
                user.Imagepath = await UplaodImage(user.ImageFile);
                _context.Add(user);
                await _context.SaveChangesAsync();

                Account account = new Account();
                account.UserId = user.Id;
                account.RoleId = 2;
                account.Username = username;
                account.Password = password;
                _context.Add(account);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetInt32("UserID", (int)user.Id);
                return RedirectToAction("Edit", "Client", new { id = user.Id });
            }
            return View(user);
        }
        private async Task<string> RegisterValidation(string username, string password, string repeatedpassword, User user)
        {
            Account UniqueUser = await _context.Accounts.Where(customer => customer.Username == username).FirstOrDefaultAsync();
            if (UniqueUser != null) return "Sorry, this email is used";
            if (username == null) return "Pleas enter your email";
            if (password == null) return "Please enter a password";
            if (repeatedpassword == null) return "Please enter the password again";
            if (password != repeatedpassword) return "Sorry, passwords must be identical";
            if (user.Firstname == null) return "Please enter your first name";
            if (user.Lastname == null) return "Please enter your last name";
            if (user.Birthdate == null) return "Please enter your birth date";
            if (user.Sex == null) return "Please enter your sex";
            return null;
        }
        public async Task<string> UplaodImage(IFormFile ImageFile)
        {
            if (ImageFile == null) return "defaultUserImage.png";
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
            string path = Path.Combine(wwwRootPath + "/Images/", fileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public IActionResult Login()
        {
            //ViewData["Authentication"] = null;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password,UserId,RoleId")]Account account)
        {
            try
            {
                Account authentication = await _context.Accounts.Where(acc => acc.Username == account.Username && acc.Password == account.Password).FirstOrDefaultAsync();
                User user = await _context.Users.Where(customer => customer.Id == authentication.UserId).FirstOrDefaultAsync();
                switch (authentication.RoleId)
                {
                    case 1:
                        {
                            HttpContext.Session.SetInt32("UserID", (int)user.Id);
                            HttpContext.Session.SetInt32("RoleID", (int)authentication.RoleId);
                            return RedirectToAction("AdminDashboard", "Admin");
                        }
                            case 2:
                        {
                            HttpContext.Session.SetInt32("UserID", (int)user.Id);
                            return RedirectToAction("Edit", "Client", new { id = user.Id });//////
                        }
                }
            }
            catch (Exception)
            {
                ViewData["Authentication"] = "Invalid email or password";
            }
            return View(nameof(Login));
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            if (HttpContext.Request.Cookies["UserID"] != null)
            {
                
                HttpContext.Response.Cookies.Delete("UserID");
            } 
            return RedirectToAction("Index", "Home");
        }
    }
}
