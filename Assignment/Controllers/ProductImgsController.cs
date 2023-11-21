using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment.Models.Context;
using Assignment.Models.DomainClass;
using Microsoft.Extensions.Hosting;

namespace Assignment.Controllers
{
    public class ProductImgsController : Controller
    {
        private readonly MyContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductImgsController(MyContext context, IWebHostEnvironment environment)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // GET: ProductImgs
        public async Task<IActionResult> Index()
        {
            var myContext = _context.ProductImgs.Include(p => p.Product);
            var listProduct = _context.Products.ToList();
            ViewData["ListProduct"] = listProduct;
            return View(await myContext.ToListAsync());
        }

        // GET: ProductImgs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductImgs == null)
            {
                return NotFound();
            }

            var productImg = await _context.ProductImgs
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImg == null)
            {
                return NotFound();
            }

            return View(productImg);
        }

        // GET: ProductImgs/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ProductImgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Url1,Url2,Url3")] ProductImg productImg, IFormFile Url1, IFormFile Url2, IFormFile Url3)
        {
            if (!ModelState.IsValid)
            {
                // Xử lý tải lên hình ảnh và lưu vào bảng ProductImg
                productImg.Url1 = await ProcessImage(Url1);
                productImg.Url2 = await ProcessImage(Url2);
                productImg.Url3 = await ProcessImage(Url3);

                _context.Add(productImg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Nếu ModelState không hợp lệ, bạn có thể cần xử lý đối với lỗi
            ViewBag.ProductID = new SelectList(_context.Products, "Id", "Name", productImg.ProductID);
            return View(productImg);
        }

        private async Task<string> ProcessImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null; // Không có tệp hoặc tệp trống
            }

            // Đổi tên tệp để tránh trùng lặp và xử lý các kí tự đặc biệt
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{fileExtension}";

            // Đường dẫn thư mục lưu trữ ảnh (chỉ là một ví dụ, bạn cần thay đổi tùy thuộc vào cấu trúc thư mục của bạn)
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads");

            // Đường dẫn đầy đủ của tệp mới
            var filePath = Path.Combine(uploadPath, newFileName);

            // Kiểm tra xem thư mục lưu trữ đã tồn tại chưa, nếu chưa thì tạo mới
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Lưu tệp vào thư mục lưu trữ
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn của tệp mới
            return $"/uploads/{newFileName}";
        }

        // GET: ProductImgs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductImgs == null)
            {
                return NotFound();
            }

            var productImg = await _context.ProductImgs.FindAsync(id);
            if (productImg == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productImg.ProductID);
            return View(productImg);
        }

        // POST: ProductImgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductID,Url1,Url2,Url3")] ProductImg productImg)
        {
            if (id != productImg.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImgExists(productImg.Id))
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
            ViewData["ProductID"] = new SelectList(_context.Products, "Id", "Description", productImg.ProductID);
            return View(productImg);
        }

        // GET: ProductImgs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductImgs == null)
            {
                return NotFound();
            }

            var productImg = await _context.ProductImgs
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImg == null)
            {
                return NotFound();
            }

            return View(productImg);
        }

        // POST: ProductImgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductImgs == null)
            {
                return Problem("Entity set 'MyContext.ProductImgs'  is null.");
            }
            var productImg = await _context.ProductImgs.FindAsync(id);
            if (productImg != null)
            {
                _context.ProductImgs.Remove(productImg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImgExists(int id)
        {
          return (_context.ProductImgs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
