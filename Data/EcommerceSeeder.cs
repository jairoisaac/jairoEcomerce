using System.Linq;

namespace jairoEcomerce.Data
{
    public class EcommerceSeeder
    {
        private readonly MyEcomerceContext ctx;

        public EcommerceSeeder(MyEcomerceContext ctx)
        {
            this.ctx = ctx;
        }
        public void Seed()
        {
            ctx.Database.EnsureCreated();
            if (ctx.Products.Any())
            {

            }
        }
    }
}
