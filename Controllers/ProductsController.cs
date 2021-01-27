using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Drawing;
using jairoEcomerce.Data;
using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jairoEcomerce.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public ProductsController(IEcommerceRepository repository, ILogger<OrdersController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            try
            {
                return Ok(await repository.GetProductsAsync());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
        }
        


        //public IActionResult Post([FromBody] Product model)
        [HttpPost]
        public ActionResult<ProductViewModel> Post([FromBody]ProductViewModel model)
        {
            try 
            {
                if (ModelState.IsValid)
                {
                    var newProduct = mapper.Map<ProductViewModel, Product>(model);
                    repository.AddProduct(newProduct);

                    if (repository.SaveAll())
                    {
                        return Created($"/api/product/{model.Name}", model);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save a new order: {ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}
