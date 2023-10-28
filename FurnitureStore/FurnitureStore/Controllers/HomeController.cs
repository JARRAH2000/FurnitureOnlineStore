using FurnitureStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FurnitureStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ModelContext _context;

		public HomeController(ILogger<HomeController> logger, ModelContext context)
		{
			_logger = logger;
			_context = context;
		}

		public IActionResult Index()
		{
			var sections = _context.Sections.ToList();
			var furnitures = _context.Furnitures.Include(f => f.Section).ToList();
			var offers = _context.Offers.Where(ofr =>ofr.StartDate<=DateTime.Today&& ofr.EndDate >= DateTime.Today).Include(offer => offer.Furniture).Include(offer => offer.Furniture.Section).ToList();
			var testimonials = _context.Testimonials.Where(test => test.Publish == "Y").Include(tst=>tst.Sender).ToList();
			var pages = _context.Pages.FirstOrDefault();
			var model = Tuple.Create<IEnumerable<Section>, IEnumerable<Furniture>, IEnumerable<Offer>, IEnumerable<Testimonial>, Pages>(sections, furnitures, offers, testimonials, pages);
			return View(model);
		}

		public IActionResult About()
		{
			var about = _context.Pages.FirstOrDefault();
			return View(about);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
