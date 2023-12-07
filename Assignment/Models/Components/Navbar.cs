using Assignment.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models.Components
{
    public class Navbar:ViewComponent
    {
        private readonly MyContext _context;

        public Navbar(MyContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            return View(_context.Brands.ToList());
        }
    }
}
