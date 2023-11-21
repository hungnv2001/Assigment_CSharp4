﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
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
        public int BrandID {  get; set; }
        [DisplayName("Thương hiệu")]
      
        public Brand Brand { get; set; }
        public ICollection<ProductImg> ProductImgs { get; set;}
        public ICollection<ProductVariant> ProductVariants { get; set;}
    }
}
