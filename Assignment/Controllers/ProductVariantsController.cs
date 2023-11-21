using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models.Context;
using Assignment.Models.DomainClass;

namespace Assignment.Controllers
{
    public class ProductVariantsController : Controller
    {
        private readonly MyContext _context;

        public ProductVariantsController(MyContext context)
        {
            _context = context;
        }

        // GET: ProductVariants
        public async Task<IActionResult> Index()
        {
            var myContext = _context.ProductVariants.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            return View(await myContext.ToListAsync());
        }

        // GET: ProductVariants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productVariant == null)
            {
                return NotFound();
            }

            return View(productVariant);
        }

        // GET: ProductVariants/Create
        public IActionResult Create()
        {
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description");
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name");
            return View();
        }

        // POST: ProductVariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductID,ColorID,SizeID,Status")] ProductVariant productVariant)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(productVariant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name", productVariant.ColorID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return View(productVariant);
        }

        // GET: ProductVariants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant == null)
            {
                return NotFound();
            }
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name", productVariant.ColorID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return View(productVariant);
        }

        // POST: ProductVariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductID,ColorID,SizeID,Status")] ProductVariant productVariant)
        {
            if (id != productVariant.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(productVariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductVariantExists(productVariant.Id))
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
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name", productVariant.ColorID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return View(productVariant);
        }

        // GET: ProductVariants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductVariants == null)
            {
                return NotFound();
            }

            var productVariant = await _context.ProductVariants
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productVariant == null)
            {
                return NotFound();
            }

            return View(productVariant);
        }

        // POST: ProductVariants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductVariants == null)
            {
                return Problem("Entity set 'MyContext.ProductVariants'  is null.");
            }
            var productVariant = await _context.ProductVariants.FindAsync(id);
            if (productVariant != null)
            {
                _context.ProductVariants.Remove(productVariant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductVariantExists(int id)
        {
          return (_context.ProductVariants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
