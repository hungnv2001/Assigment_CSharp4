using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class ProductImg
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Tên sản phẩm")]
        public Guid ProductID {  get; set; }

        [ForeignKey("ProductID")]
        public Product Product { get; set; }
        [MaxLength(100)]
		[DisplayName("Ảnh 1")]
		public string Url1 { get; set; }
        [MaxLength(100)]
		[DisplayName("Ảnh 2")]
		public string Url2 { get; set; }
        [MaxLength(100)]
		[DisplayName("Ảnh 3")]
		public string Url3 { get; set; }
    }
}
