using jairoEcomerce.Data.Entities;
using System.Collections.Generic;

namespace jairoEcomerce.Data
{
    public interface IEcommerceRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        public bool SaveAll();
        void AddOrder(Order model);
    }
}