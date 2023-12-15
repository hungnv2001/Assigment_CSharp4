using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models.Context;
using Assignment.Models.DomainClass;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Query;

namespace Assignment.Controllers
{
    public class CartItemsController : Controller
    {
        private readonly MyContext _context;

        public CartItemsController(MyContext context)
        {
            _context = context;
        }

     

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Not_log");
            }
            var y = _context.CartItems.ToList();
            var myContext = _context.CartItems.Include(p => p.ProductVariant).ThenInclude(p => p.Size).Include(p => p.ProductVariant).ThenInclude(p => p.Color).Include(p => p.ProductVariant).ThenInclude(p => p.Product).Include(p=>p.Cart);
            var x = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result= new List<CartItem>();
            if (x!=null)
            {
                
            result = myContext.Where(p => p.Cart.CustomerID.ToString() == x.ToString()).ToList();
            }

            if (result.Count==0)
            {
                return View("Emty");
            }
            return View(result);
            
            
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.ProductVariant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // GET: CartItems/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CartID"] = new SelectList(_context.Carts, "Id", "Id");
            ViewData["ProductVariantID"] = new SelectList(_context.ProductVariants, "Id", "Id");
            return View();
        }

        //POST: CartItems/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

       [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantiy,ProductVariantID,CartID")] CartItem cartItem)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(cartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartID"] = new SelectList(_context.Carts, "Id", "Id", cartItem.CartID);
            ViewData["ProductVariantID"] = new SelectList(_context.ProductVariants, "Id", "Id", cartItem.ProductVariantID);
            return View(cartItem);
        }
        [HttpPost]
		    public async Task<IActionResult> AddCart(Guid IdSP)
		    {

                if (IdSP != null && HttpContext.User.Identity.IsAuthenticated)
                {
				    var x = IdSP;
				    string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var idCart = _context.Carts.First(p => p.CustomerID.ToString() == userid.ToString()).Id;
                    var checkValid=_context.CartItems.FirstOrDefault(p=>p.CartID == idCart && p.ProductVariantID==IdSP);
                    if (checkValid != null)
                    {
                        checkValid.Quantiy++;
                        _context.Update(checkValid);
                        _context.SaveChanges();
                    }
                    else
                    {
                        var newCartItem = new CartItem()
                        {
                            CartID = idCart,
                            Quantiy = 1,
                            ProductVariantID = IdSP

                        };
                        _context.Add(newCartItem);
                        _context.SaveChanges();
                    }
				
			    }
			    string returnUrl = HttpContext.Request.Headers["Referer"].ToString();
			    return Redirect(returnUrl);
		    }
		// GET: CartItems/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            ViewData["CartID"] = new SelectList(_context.Carts, "Id", "Id", cartItem.CartID);
            ViewData["ProductVariantID"] = new SelectList(_context.ProductVariants, "Id", "Id", cartItem.ProductVariantID);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Quantiy,ProductVariantID,CartID")] CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.Id))
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
            ViewData["CartID"] = new SelectList(_context.Carts, "Id", "Id", cartItem.CartID);
            ViewData["ProductVariantID"] = new SelectList(_context.ProductVariants, "Id", "Id", cartItem.ProductVariantID);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.CartItems == null)
            {
                return NotFound();
            }

            var cartItem = await _context.CartItems
                .Include(c => c.Cart)
                .Include(c => c.ProductVariant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CartItems == null)
            {
                return Problem("Entity set 'MyContext.CartItems'  is null.");
            }
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartItemExists(Guid id)
        {
          return (_context.CartItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
