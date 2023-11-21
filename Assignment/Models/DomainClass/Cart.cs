using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int Quantity {  get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public User User { get; set; }
    }
}
