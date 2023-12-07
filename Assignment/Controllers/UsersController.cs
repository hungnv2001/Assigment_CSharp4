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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Assignment.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyContext _context;

        public UsersController(MyContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var myContext = _context.Users.Include(u => u.Role);
            return View(await myContext.ToListAsync());
        }
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User Inuser)
        {
            var user = _context.Users.Include(p => p.Role).FirstOrDefault(p => p.UserName == Inuser.UserName && p.Password == Inuser.Password&&p.Status!=0);
            if (user == null || _context.Users == null)
            {
                return View();
            }
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.Role.Name),

        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
           CookieAuthenticationDefaults.AuthenticationScheme,
           new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Products");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return View("Login");
        }



        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }
        // GET: Users/Create
        [HttpGet]
        public IActionResult Register(User? _user, string? error)
        {
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,RoleID,FullName,Email,PhoneNumber,Address,Status")] User user)
        {
            if (_context.Users.FirstOrDefault(p => p.UserName == user.UserName) != null)
            {
                return RedirectToAction("Resignt", user);
            }
            if (!ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                //   user.RoleID = new Guid(1235-4444-);

                _context.Add(user);
                await _context.SaveChangesAsync();
                var cart = new Cart()
                {
                    Id = Guid.NewGuid(),
                    CustomerID = user.Id,
                    Quantity = 0

                };
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name", user.RoleID);
            return RedirectToAction("Resignt", user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name", user.RoleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,Password,RoleID,FullName,Email,PhoneNumber,Address,Status")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleID"] = new SelectList(_context.Roles, "Id", "Name", user.RoleID);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MyContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
