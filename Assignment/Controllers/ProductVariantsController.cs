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
        public async Task<IActionResult> Index(Guid? id)
        {
            var myContext = _context.ProductVariants.Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            if (id == null || _context.ProductVariants == null)
            {
                return View(await myContext.ToListAsync());
            }
            var x = myContext.Where(p => p.ProductID == id);
            ViewBag.ProductID = id;

            //if (id != null)
            //{
            //	return RedirectToAction("Create", new { id = id.Value });
            //}
            return View(await x.ToListAsync());
        }

        // GET: ProductVariants/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        public IActionResult Create(Guid id)
        {
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name");
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name");
            ViewData["ProductIdFromIndex"] = id;
            return View();
        }

        // POST: ProductVariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ColorID,SizeID,Quantity,Status")] ProductVariant productVariant)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(productVariant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = productVariant.ProductID });
            }

            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name", productVariant.ColorID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return RedirectToAction("Index", new { id = productVariant.ProductID });
        }

        // GET: ProductVariants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return View(productVariant);
        }

        // POST: ProductVariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductID,ColorID,SizeID,Quantity,Status")] ProductVariant productVariant)
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
                return RedirectToAction("Index", new { id = productVariant.ProductID });
            }
            ViewData["ColorID"] = new SelectList(_context.Colors, "Id", "Name", productVariant.ColorID);
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productVariant.ProductID);
            ViewData["SizeID"] = new SelectList(_context.Sizes, "ID", "Name", productVariant.SizeID);
            return RedirectToAction("Index", new { id = productVariant.ProductID });
        }

        // GET: ProductVariants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
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
            return RedirectToAction("Index", new { id = productVariant.ProductID });
        }

        private bool ProductVariantExists(Guid id)
        {
            return (_context.ProductVariants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
