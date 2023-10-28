using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FurnitureStore.Controllers
{
    public class ActivitesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ActivitesController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowRatings()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            if (id == null) return RedirectToAction("Login", "Authentication");
            var ratings = await _context.Ratings.Where(rating => rating.UserId == id).Include(rating => rating.Furniture).Include(furniture => furniture.Furniture.Section).ToListAsync();
            return View(ratings);
        }


        public async Task<IActionResult> ShowPreference()
        {
            HttpContext.Session.Remove("FurnitureId");
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            if (id == null) return RedirectToAction("Login", "Authentication");
            var favourites = await _context.Favourites.Where(rating => rating.UserId == id).Include(rating => rating.Furniture).Include(furniture => furniture.Furniture.Section).ToListAsync();

            return View(favourites);
        }


        public async Task<IActionResult> ShowComments()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            if (id == null) return RedirectToAction("Login", "Authentication");
            var comments = await _context.Comments.Where(rating => rating.UserId == id).Include(rating => rating.Furniture).Include(furniture => furniture.Furniture.Section).ToListAsync();
            return View(comments);
        }

        public async Task<IActionResult> ShowBag()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal? id = HttpContext.Session.GetInt32("UserID");
            var bag = await _context.Bags.Where(bag => bag.UserId == id && bag.Quantity > 0).Include(bag => bag.Furniture).Include(bag => bag.Furniture.Section).ToListAsync();
            return View(bag);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(string cardNumber, string cvc, decimal total)
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal id = (decimal)HttpContext.Session.GetInt32("UserID");
            var visa = _context.Banks.Where(card => card.CardCvc == cvc && card.CardNumber == cardNumber).FirstOrDefault();
            if (visa == null)
            {
                TempData["Error"] = "Wrong card information";
                return RedirectToAction("ShowBag", "Activites");
            }
            if (visa.Balance < total)
            {
                TempData["Error"] = "Balance is not enough";
                return RedirectToAction("ShowBag", "Activites");
            }
            visa.Balance -= total;
            Payment payment = new Payment();
            payment.BillTime = DateTime.Now;
            payment.UserId = id;

            _context.Add(payment);
            await _context.SaveChangesAsync();

            string table = "<table border=\"1\"><thead><tr><th>Name</th><th>Section</th><th>Price</th><th>Quantity</th><th>Total</th></thead><tbody>";
            var bag = await _context.Bags.Where(bag => bag.UserId == id).Include(bag => bag.Furniture).Include(bag => bag.Furniture.Section).ToListAsync();
            foreach (Bag product in bag)
            {
                Furniture furniture = await _context.Furnitures.Where(fur => fur.Id == product.FurnitureId).Include(fur => fur.Section).FirstOrDefaultAsync();
                decimal quantity = 0;
                if (furniture.Quantity >= product.Quantity) quantity = product.Quantity;
                else
                {
                    quantity = (decimal)furniture.Quantity;
                    TempData["Availabe"] = "Some products were not available in the required quantities. Only the available quantity was calculated in the invoice";
                }

                furniture.Quantity -= quantity;


                _context.Update(furniture);

                var checkOffer = _context.Offers.Where(offer => offer.FurnitureId == furniture.Id && offer.EndDate >= DateTime.Today && offer.StartDate <= DateTime.Today).OrderByDescending(offer => offer.Id).FirstOrDefault();
                ProductPayment productPayment = new ProductPayment();
                if (checkOffer != null)
                {
                    productPayment.Price = furniture.Price - (furniture.Price * checkOffer.Percentage / 100);
                }
                else { productPayment.Price = furniture.Price; }

                productPayment.Quantity = quantity;
                productPayment.PaymentId = payment.Id;
                productPayment.FurnitureId = furniture.Id;
                _context.Add(productPayment);
                if (quantity != 0)
                    table += $"<tr><td>{furniture.Name}</td><td>{furniture.Section.Name}</td><td>{productPayment.Price}</td><td>{productPayment.Quantity}</td><td>{productPayment.Price * productPayment.Quantity}</td></tr>";
                product.Quantity -= quantity;
                if (product.Quantity == 0)
                    _context.Remove(product);
                else _context.Update(product);
                await _context.SaveChangesAsync();
            }
            table += $"<tr><td></td><td></td><td></td><td></td><td>{total}</td></tr></tbody></table>";
            table += $"<p>Time : {payment.BillTime}</p>";


            HtmlToPdfConverter htmlToPdfConverter = new HtmlToPdfConverter();
            PdfDocument pdf = htmlToPdfConverter.Convert(table, "");
            FileStream fileStream = new FileStream($"bill_{payment.Id}.pdf", FileMode.CreateNew, FileAccess.ReadWrite);
            pdf.Save(fileStream);
            pdf.Close(true);
            fileStream.Close();

            SmtpClient emailBill = new SmtpClient("smtp-mail.outlook.com", 587);
            NetworkCredential credential = new NetworkCredential("email@outlook.com", "password");
            emailBill.Credentials = credential;
            emailBill.EnableSsl = true;
            MailMessage mail = new MailMessage("email@outlook.com", _context.Accounts.Where(acc => acc.UserId == id).FirstOrDefault().Username, "Bill", table);
            mail.IsBodyHtml = true;
            mail.Attachments.Add(new Attachment($"bill_{payment.Id}.pdf"));
            TempData["EmailStatus"] = "Bill sent by email";
            try
            {
                emailBill.Send(mail);
            }
            catch (Exception)
            {
                TempData["EmailStatus"] = "Email sent failed";
            }
            TempData["PayStatus"] = "Parchuse Done";
            return RedirectToAction("ShowBag", "Activites");//modify
        }


        public async Task<IActionResult> ShowPayments()
        {
            if (HttpContext.Session.GetInt32("UserID") == null) RedirectToAction("Login", "Authentication");
            decimal id = (decimal)HttpContext.Session.GetInt32("UserID");
            var payments = await _context.Payments.Where(pay => pay.UserId == id).OrderByDescending(pay => pay.BillTime).ToListAsync();
            return View(payments);
        }
    }
}
