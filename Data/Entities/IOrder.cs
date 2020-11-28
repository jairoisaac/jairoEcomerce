using System;
using System.Collections.Generic;

namespace jairoEcomerce.Data.Entities
{
    public interface IOrder
    {
        int Id { get; set; }
        ICollection<OrderItem> Items { get; set; }
        DateTime OrderDate { get; set; }
        string OrderNumber { get; set; }
    }
}