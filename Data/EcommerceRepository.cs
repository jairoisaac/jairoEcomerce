using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
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

        public void AddOrder(Order model)
        {
            foreach (var item in model.Items)
            {
                item.Product = ctx.Products.Find(item.Product.Id);
            }

            ctx.Add(model);
            //foreach (var item in model.Items)
            //{
            //    ctx.OrderItems.Add(item);
            //}
            //if model is an order => add orderItems.

        }
       
        public void AddProduct(Product model)
        {
            ctx.Add(model);
            //throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            return ctx.Products.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
