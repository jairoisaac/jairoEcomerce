using System.ComponentModel.DataAnnotations.Schema;

namespace jairoEcomerce.Data.Entities
{
    //[Table("OrderItem")]
    public class OrderItem // : IOrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
    }
}