using jairoEcomerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jairoEcomerce.Data
{
    public class EcommerceRepository : IEcommerceRepository
    {
        private readonly MyEcomerceContext ctx;
        public EcommerceRepository(MyEcomerceContext ctx)
        {
            this.ctx = ctx;
        }
        public IEnumerable<Product> GetProducts()
        {
            return ctx.Products
                .OrderBy(p => p.Name)
                .ToList();
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return ctx.Products
                .Where(p => p.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }
    }
}
