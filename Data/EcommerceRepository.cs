using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jairoEcomerce.Data
{
    public class EcommerceRepository : IEcommerceRepository
    {
        private readonly MyEcomerceContext ctx;
        private readonly ILogger<EcommerceRepository> logger;
        public EcommerceRepository(MyEcomerceContext ctx, ILogger<EcommerceRepository> logger)
        {
            this.logger = logger;
            this.ctx = ctx;
        }


        public  async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var result = ctx.Products.OrderBy(p => p.Name).ToList();
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}"); 
                return null;
            }
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

        public async Task<bool> SaveAllAsync()
        {
            //TO DO:Turn this into an async operation
            return (await ctx.SaveChangesAsync()) > 0;
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
        }
       
        public void AddProduct(Product model)
        {
            ctx.Add(model);
            //throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            try
            {
                var result = ctx.Products.Where(p => p.Id == id).FirstOrDefault();
                return await Task.FromResult(result); 
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public void Delete(Product model)
        {
            logger.LogInformation($"Removing Product");
            ctx.Remove(model);
        }

    }
}
