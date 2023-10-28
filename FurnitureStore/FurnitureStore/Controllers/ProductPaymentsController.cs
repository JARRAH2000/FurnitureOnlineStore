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
    public class ProductPaymentsController : Controller
    {
        private readonly ModelContext _context;

        public ProductPaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProductPayments
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProductPayments.Include(p => p.Furniture).Include(p => p.Payment);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductPayments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments
                .Include(p => p.Furniture)
                .Include(p => p.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPayment == null)
            {
                return NotFound();
            }

            return View(productPayment);
        }

        // GET: ProductPayments/Create
        public IActionResult Create()
        {
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath");
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id");
            return View();
        }

        // POST: ProductPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity,Price,PaymentId,FurnitureId")] ProductPayment productPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", productPayment.FurnitureId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", productPayment.PaymentId);
            return View(productPayment);
        }

        // GET: ProductPayments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments.FindAsync(id);
            if (productPayment == null)
            {
                return NotFound();
            }
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", productPayment.FurnitureId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", productPayment.PaymentId);
            return View(productPayment);
        }

        // POST: ProductPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Quantity,Price,PaymentId,FurnitureId")] ProductPayment productPayment)
        {
            if (id != productPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPaymentExists(productPayment.Id))
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
            ViewData["FurnitureId"] = new SelectList(_context.Furnitures, "Id", "Imagepath", productPayment.FurnitureId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", productPayment.PaymentId);
            return View(productPayment);
        }

        // GET: ProductPayments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments
                .Include(p => p.Furniture)
                .Include(p => p.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPayment == null)
            {
                return NotFound();
            }

            return View(productPayment);
        }

        // POST: ProductPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productPayment = await _context.ProductPayments.FindAsync(id);
            _context.ProductPayments.Remove(productPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPaymentExists(decimal id)
        {
            return _context.ProductPayments.Any(e => e.Id == id);
        }
    }
}
