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
using System.Security.Policy;
using Assignment.Models;
using SQLitePCL;
using Assignment.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Controllers
{
	public class ProductsController : Controller
	{
		private readonly MyContext _context;
		private readonly IWebHostEnvironment _environment;

		public ProductsController(MyContext context, IWebHostEnvironment environment)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_environment = environment ?? throw new ArgumentNullException(nameof(environment));
		}

		// GET: Products
		public async Task<IActionResult> Index()
		{
			var myContext = _context.Products.Include(p => p.Brand).Include(p => p.ProductVariants).ThenInclude(p => p.Color).Include(p => p.ProductVariants).ThenInclude(p => p.Size).Where(p => p.Status == 1).ToList();
			myContext = myContext.Where(p => p.ProductVariants.Any(p => p.Quantity > 0 && p.Status == 1)).ToList();
			if (myContext != null)
			{
				return View(myContext);
			}
			return NotFound();
		}
		public async Task<IActionResult> ListProduct()
		{
			var myContext = _context.Products.Include(p => p.Brand).Include(p => p.ProductImgs).Include(p => p.ProductVariants).ThenInclude(p => p.Color).Include(p => p.ProductVariants).ThenInclude(p => p.Size).Where(p => p.Status == 1).ToList();
			myContext = myContext.Where(p => p.ProductVariants.Any(p => p.Quantity > 0 && p.Status == 1)).ToList();
			
			if (myContext != null)
			{
				return View(myContext);
			}
			return NotFound();
		}

		[HttpPost]
		[HttpGet]
		public async Task<IActionResult> Shop(string? keyword, int? id)
		{
			if (id == null)
			{
				ViewBag.Page = 1;
			}
			else
			{
				ViewBag.Page = id;
			}
			if (keyword == null)
			{
				keyword = "";
			}
			ViewBag.keyword = keyword;
			var myContext = _context.Products.Include(p => p.Brand).Include(p => p.ProductVariants).ThenInclude(p => p.Color).Include(p => p.ProductVariants).ThenInclude(p => p.Size).Where(p => p.Status == 1).ToList();
			myContext = myContext.Where(p => p.ProductVariants.Count > 0).Where(p => p.ProductVariants.Any(p => p.Quantity > 0 && p.Status == 1)).ToList();
			myContext = myContext.Where(p => p.Name.ToLower().Contains(keyword.ToLower()) || p.Brand.Name.ToLower().Contains(keyword.ToLower())).ToList();
			return View(myContext);

		}
		// GET: Products/Details/5
		public async Task<IActionResult> Details(Guid? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Brand).Include(p => p.ProductVariants).Include(p => p.ProductImgs)
				.FirstOrDefaultAsync(m => m.Id == id);
			var dataVariant = product.ProductVariants;
			foreach (var item in dataVariant)
			{
				item.Size = _context.Sizes.FirstOrDefault(m => m.ID == item.SizeID);
				item.Color = _context.Colors.FirstOrDefault(m => m.Id == item.ColorID);
			}
			product.ProductVariants = dataVariant;
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// GET: Products/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			ViewData["BrandID"] = new SelectList(_context.Brands, "Id", "Name");

			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Create(Models.ViewModel.Product_Img viewModel, IFormFile ImageMainUrl, IFormFile Url1, IFormFile Url2, IFormFile Url3)
		{
			var product = new Product();
			if (!ModelState.IsValid)
			{
				var genID = Guid.NewGuid();
				product = new Product()
				{
					Id = genID,
					Name = viewModel.Name,
					Brand = viewModel.Brand,
					BrandID = viewModel.BrandID,
					Description = viewModel.Description,
					Price = viewModel.Price,
					Status = viewModel.Status,
				};
				product.ImageMainUrl = await ProcessImage(ImageMainUrl);
				_context.Add(product);

				await _context.SaveChangesAsync();
				var productImg = new ProductImg()
				{
					ProductID = genID,

				};
				productImg.Url1 = await ProcessImage(Url1);
				productImg.Url2 = await ProcessImage(Url2);
				productImg.Url3 = await ProcessImage(Url3);
				_context.Add(productImg);

				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}

			// Nếu ModelState không hợp lệ, bạn có thể cần thực hiện xử lý đối với lỗi
			ViewData["BrandID"] = new SelectList(_context.Brands, "Id", "Name", product.BrandID);
			return View(product);
		}


		// GET: Products/Edit/5
		public async Task<IActionResult> Edit(Guid id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FindAsync(id);
			var viewProduct = new Product_Img()
			{
				Id = id,
				BrandID = product.BrandID,
				Description = product.Description,
				Status = product.Status,
				Price = product.Price,
				ImageMainUrl = product.ImageMainUrl,
				Name = product.Name,
				Brand = product.Brand

			};
			var pro_img = _context.ProductImgs.FirstOrDefault(p => p.ProductID == id);

			if (pro_img != null)
			{
				viewProduct.Url1 = pro_img.Url1;
				viewProduct.Url2 = pro_img.Url2;
				viewProduct.Url3 = pro_img.Url3;
			}
			if (product == null)
			{
				return NotFound();
			}
			ViewData["BrandID"] = new SelectList(_context.Brands, "Id", "Name", product.BrandID);
			return View(viewProduct);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Guid id, Models.ViewModel.Product_Img viewModel, IFormFile ImageMainUrl, IFormFile Url1, IFormFile Url2, IFormFile Url3)
		{


			var product = new Product();
			if (!ModelState.IsValid)
			{
				try
				{
					var productI = new Product();


					if (ImageMainUrl != null)
					{
						viewModel.ImageMainUrl = await ProcessImage(ImageMainUrl);
					}
					else
					{
						productI = _context.Products.FirstOrDefault(p => p.Id == id);
					}
					product = new Product()
					{
						Id = id,
						Name = viewModel.Name,
						Brand = viewModel.Brand,
						BrandID = viewModel.BrandID,
						Description = viewModel.Description,
						Price = viewModel.Price,
						Status = viewModel.Status,

					};
					if (productI.ImageMainUrl != null)
					{
						product.ImageMainUrl = productI.ImageMainUrl;
					}
					else
					{
						product.ImageMainUrl = viewModel.ImageMainUrl;
					}
					var existingProduct = _context.Products.Find(product.Id);
					if (existingProduct != null)
					{
						_context.Entry(existingProduct).CurrentValues.SetValues(product);
					}
					else
					{
						_context.Add(product);
					}
					await _context.SaveChangesAsync();



					var productImg = _context.ProductImgs.FirstOrDefault(p => p.ProductID == id);
					if (Url1 != null)
					{
						productImg.Url1 = await ProcessImage(Url1);
					}
					if (Url2 != null)
					{
						productImg.Url2 = await ProcessImage(Url2);
					}
					if (Url3 != null)
					{
						productImg.Url3 = await ProcessImage(Url3);
					}


					_context.Update(productImg);

					await _context.SaveChangesAsync();

					// code 
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ProductExists(product.Id))
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
			ViewData["BrandID"] = new SelectList(_context.Brands, "Id", "Name", product.BrandID);
			return View(product);
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
		// GET: Products/Delete/5
		public async Task<IActionResult> Delete(Guid? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Brand)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(Guid id)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'MyContext.Products'  is null.");
			}
			var product = await _context.Products.FindAsync(id);
			if (product != null)
			{
				_context.Products.Remove(product);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ProductExists(Guid id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
