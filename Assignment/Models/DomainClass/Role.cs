using System.ComponentModel.DataAnnotations;

namespace Assignment.Models.DomainClass
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
