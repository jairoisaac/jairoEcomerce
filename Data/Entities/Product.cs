using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jairoEcomerce.Data.Entities
{
    //[Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
