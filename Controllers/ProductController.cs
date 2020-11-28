using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jairoEcomerce.Data;
using jairoEcomerce.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jairoEcomerce.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrdersController> logger;
        
        public ProductController(IEcommerceRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            try
            {
                return Ok(repository.GetProducts());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }
    }
}
