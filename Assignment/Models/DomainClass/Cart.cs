using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models.DomainClass
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity {  get; set; }
        [DisplayName("Tên khách hàng")]
        public Guid CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public User User { get; set; }
    }
}
