using Assignment.Models.DomainClass;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Assignment.Models.ViewModel
{
    public class Product_Img
    {
        public Guid Id { get; set; }
        [DisplayName("Tên sản phẩm")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Giá bán")]
        public float Price { get; set; }
      
        [DisplayName("Hình ảnh chính")]
        public string? ImageMainUrl { get; set; }
        [Required]
        [DisplayName("Thương hiệu")]
        public Guid BrandID { get; set; }
        [DisplayName("Thương hiệu")]
        public Brand Brand { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        [DisplayName("Ảnh 1")]
        public string? Url1 { get; set; }
        [DisplayName("Ảnh 2")]
        public string? Url2 { get; set; }
        [DisplayName("Ảnh 3")]
        public string? Url3 { get; set; }
    }
}
