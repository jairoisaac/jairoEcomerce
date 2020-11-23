using jairoEcomerce.Data.Entities;
using System.Collections.Generic;

namespace jairoEcomerce.Data
{
    public interface IEcommerceRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        public bool SaveAll();
    }
}