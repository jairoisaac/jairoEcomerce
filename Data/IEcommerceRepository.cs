using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jairoEcomerce.Data
{
    public interface IEcommerceRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        IEnumerable<Product> GetProductsByCategory(string category);
        Task<Product> GetProductAsync(int id);
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int id);
        public bool SaveAll();
        void AddOrder(Order model);
        void AddProduct(Product model);
    }
}