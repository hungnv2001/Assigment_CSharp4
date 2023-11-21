using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName {  get; set; }
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        [Required]
        public int RoleID {  get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName  { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(250)]
        public string Address { get; set;}
        public int Status {  get; set; }
        public ICollection<Cart> Carts { get; set; }
        public ICollection<Invoice> InVoices { get;}
    }
}
