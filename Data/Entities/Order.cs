using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jairoEcomerce.Data.Entities
{
    //[Table("Order")]
    public class Order //: IOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
