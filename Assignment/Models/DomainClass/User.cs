using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Tên đăng nhập")]
        public string UserName {  get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Chức vụ")]
        public Guid RoleID {  get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Họ và tên")]
        public string FullName  { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(250)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set;}

        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Invoice> InVoices { get;}
    }
}
