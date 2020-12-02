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
       public IEnumerable<Order> GetOrders()
        {
            return ctx.Orders.Select(o => new Order
            {
                Id        = o.Id,
                OrderDate = o.OrderDate,
                OrderNumber = o.OrderNumber,
                Items = o.Items.Select(i => new OrderItem
                {
                    Id        = i.Id,
                    Product   = i.Product,
                    Quantity  = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();
        }

        public bool SaveAll()
        {
            return ctx.SaveChanges() > 0;
        }

        public Order GetOrderById(int id)
        {
            return ctx.Orders.Where(or => or.Id == id)
                .Select(o => new Order
            {
                Id        = o.Id,
                OrderDate = o.OrderDate,
                OrderNumber = o.OrderNumber,
                Items = o.Items.Select(i => new OrderItem
                {
                    Id = i.Id,
                    Product = i.Product,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).FirstOrDefault();
        }

        public void AddEntity(object model)
        {
            ctx.Add(model);
        }
    }
}
