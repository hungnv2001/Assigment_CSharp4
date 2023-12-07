using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Giá bán")]
        public float Price { get; set; }
        [Required]
        [DisplayName("Hình ảnh chính")]
        public string ImageMainUrl {  get; set; }
        [Required]
        [DisplayName("Thương hiệu")]
        public Guid BrandID {  get; set; }
        [DisplayName("Thương hiệu")]
        public Brand Brand { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        public ICollection<ProductImg> ProductImgs { get; set;} = new List<ProductImg>();
		public ICollection<ProductVariant> ProductVariants { get; set;}

		internal Product Where(Func<object, bool> value)
		{
			throw new NotImplementedException();
		}
	}
}
