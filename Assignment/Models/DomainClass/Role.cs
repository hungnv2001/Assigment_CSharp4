using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Tên chức vụ")]
        public string Name { get; set; }
        [DisplayName("Trạng thái")]
        public int Status {  get; set; }
        public ICollection<User> Users { get; set; }
    }
}
