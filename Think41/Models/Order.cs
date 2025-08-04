using System.ComponentModel.DataAnnotations;

namespace Think41.Models
{
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public int User_Id { get; set; }  // Foreign key to Customer.Id
        public string Status { get; set; }
        public string Gender { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Returned_At { get; set; }
        public DateTime? Shipped_At { get; set; }
        public DateTime? Delivered_At { get; set; }
        public int Num_Of_Item { get; set; }

        public Customer Customer { get; set; }
    }

}
