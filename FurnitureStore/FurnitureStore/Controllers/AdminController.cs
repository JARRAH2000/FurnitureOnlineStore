using FurnitureStore.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminController(ModelContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetInt32("UserID") == null || HttpContext.Session.GetInt32("RoleID") == null) return RedirectToAction("Login", "Authentication");
            var users = _context.Users.ToList();
            var productPayments = _context.ProductPayments.Include(product => product.Furniture).Include(section => section.Furniture.Section).ToList();
            var orders = _context.Payments.ToList();
            var sections = _context.Sections.ToList();

            DateTime today = DateTime.Now.Date;
            DateTime todayStart = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);
            DateTime todayEnd = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59);

            var messages = _context.Messages.Where(msg => msg.SendTime >= todayStart && msg.SendTime <= todayEnd).ToList();
            var testimonials = _context.Testimonials.Where(tst => tst.SendTime >= todayStart && tst.SendTime <= todayEnd).ToList();
            var offers = _context.Offers.Include(product => product.Furniture).Include(section => section.Furniture.Section).ToList();


            ViewBag.Labels = new List<string> { "Male", "Female" };
            ViewBag.Data = new List<int> { users.Where(user => user.Sex == "M").Count(), users.Where(user => user.Sex == "F").Count() };


            List<int> Orders = new List<int>();
            List<int> Amounts = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                Orders.Add(_context.Payments.Where(p => p.BillTime.Month == i).Count());
                Amounts.Add((int)_context.ProductPayments.Where(p => p.Payment != null && p.Payment.BillTime.Month == i).Sum(p => p.Quantity));
            }
            ViewBag.Orders = Orders;
            ViewBag.Amounts = Amounts;


            var furnitures = _context.Furnitures.Include(f => f.Section).ToList();
            var topSections = from pp in productPayments
                              join f in furnitures on pp.FurnitureId equals f.Id
                              join s in sections on f.SectionId equals s.Id
                              group pp by s.Name into g
                              select new SectionReport
                              {
                                  Name = g.Key,
                                  TotalSales = (decimal)g.Sum(p => p.Quantity * p.Price)
                              };

            var topFurnitures = from pp in productPayments
                                join f in furnitures on pp.FurnitureId equals f.Id
                                group pp by f.Id into g
                                select new FurnitureReport
                                {
                                    Name = furnitures.Where(fur => fur.Id == g.Key).FirstOrDefault()?.Name,
                                    Category = furnitures.Where(f => f.Id == g.Key).FirstOrDefault().Section.Name,
                                    Price = (decimal)furnitures.Where(fur => fur.Id == g.Key).FirstOrDefault()?.Price,
                                    TotalSales = (decimal)g.Sum(p => p.Quantity * p.Price)
                                };

            var AdminIndex = Tuple.Create<IEnumerable<User>, IEnumerable<ProductPayment>, IEnumerable<Payment>, IEnumerable<SectionReport>, IEnumerable<Message>, IEnumerable<Testimonial>, IEnumerable<FurnitureReport>>(users, productPayments, orders, topSections, messages, testimonials, topFurnitures);
            return View(AdminIndex);
        }


        
        public async Task<IActionResult> ListOfUsers()
        {
            return View(await _context.Accounts.Include(acc=>acc.User).ToListAsync());
        }

        public async Task<IActionResult> Reports()
        {
            
            var sections = await _context.Sections.ToListAsync();
            var furnitrues = await _context.Furnitures.Include(fur=>fur.Section).ToListAsync();
            var productPayments = await _context.ProductPayments.Include(pay => pay.Furniture).Include(pay => pay.Furniture.Section).ToListAsync();
            var payments = await _context.Payments.ToListAsync();
            var tuple = Tuple.Create<IEnumerable<Section>, IEnumerable<Furniture>, IEnumerable<ProductPayment>, IEnumerable<Payment>>(sections, furnitrues, productPayments, payments);
            return View(tuple);
        }

        [HttpPost]
        public async Task<IActionResult> Reports(DateTime? dateFrom, DateTime? dateTo)
        {
            var sections = await _context.Sections.ToListAsync();
            var furnitrues = await _context.Furnitures.Include(fur => fur.Section).ToListAsync();
            var productPayments = await _context.ProductPayments.Include(pay => pay.Furniture).Include(pay => pay.Furniture.Section).ToListAsync();

            if (dateTo != null)
                dateTo = new DateTime(dateTo.Value.Year, dateTo.Value.Month, dateTo.Value.Day, 23, 59, 59);

            if (dateFrom != null && dateTo != null)
                productPayments = await _context.ProductPayments.Where(pay => pay.PaymentId != null && pay.Payment.BillTime.Date >= dateFrom.Value.Date && pay.Payment.BillTime.Date <= dateTo.Value.Date).Include(pay => pay.Furniture).Include(pay => pay.Furniture.Section).ToListAsync();
            else if (dateFrom != null)
                productPayments = await _context.ProductPayments.Where(pay => pay.PaymentId != null && pay.Payment.BillTime.Date >= dateFrom.Value.Date).Include(pay => pay.Furniture).Include(pay => pay.Furniture.Section).ToListAsync();
            else if (dateTo != null)
                productPayments = await _context.ProductPayments.Where(pay => pay.PaymentId != null && pay.Payment.BillTime.Date <= dateTo.Value.Date).Include(pay => pay.Furniture).Include(pay => pay.Furniture.Section).ToListAsync();

            var payments = await _context.Payments.ToListAsync();
            var tuple = Tuple.Create<IEnumerable<Section>, IEnumerable<Furniture>, IEnumerable<ProductPayment>, IEnumerable<Payment>>(sections, furnitrues, productPayments, payments);
            return View(tuple);
        }
    }
}
