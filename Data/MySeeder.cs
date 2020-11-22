using jairoEcomerce.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace jairoEcomerce.Data
{
    public class MySeeder
    {
        private readonly MyEcomerceContext ctx;
        private readonly IWebHostEnvironment hosting;

        public MySeeder(MyEcomerceContext ctx, IWebHostEnvironment hosting)
        {
            this.hosting = hosting;
            this.ctx = ctx;
        }

        public void Seed() 
        {
            ctx.Database.EnsureCreated();
            if (!ctx.Products.Any())
            {
                var filepath = Path.Combine(hosting.ContentRootPath, "Data/myProducts.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert
                    .DeserializeObject<IEnumerable<Product>>(json);
                ctx.Products.AddRange(products);
                //Need to create simple data
                var order = ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null) 
                {
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }
                ctx.SaveChanges();
            }
        }
    }
}
