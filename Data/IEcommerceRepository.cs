using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
using System.Collections.Generic;

namespace jairoEcomerce.Data
{
    public interface IEcommerceRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        Product GetProduct(int id);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        public bool SaveAll();
        void AddOrder(Order model);
        void AddProduct(Product model);
    }
}