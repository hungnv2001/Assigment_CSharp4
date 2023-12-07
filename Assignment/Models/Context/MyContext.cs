using Assignment.Models.DomainClass;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Assignment.Models.ViewModel;

namespace Assignment.Models.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        public MyContext()
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<DomainClass.Color> Colors { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductImg> ProductImgs { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get;        set; }
        public DbSet<DomainClass.Size> Sizes { get; set; }
        public DbSet<Assignment.Models.ViewModel.Product_Img>? Product_Img { get; set; }

    }
}
