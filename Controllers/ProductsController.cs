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
using Microsoft.AspNetCore.Http;

namespace jairoEcomerce.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<ProductsController> logger;
        private readonly IMapper mapper;

        public ProductsController(IEcommerceRepository repository, ILogger<ProductsController> logger,
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                return Ok(await repository.GetProductAsync(id));
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get produc: {ex}");
                return BadRequest("Failed to get produc");
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

                    if (repository.SaveAll()) { 
                    
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
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductViewModel>> Put(int id, [FromBody]ProductViewModel model)
        {
            try
            {
                var oldProduct = await repository.GetProductAsync(id);
                if (oldProduct == null) return NotFound($"Cold not find product for that {id}");
                mapper.Map(model, oldProduct);
                if (await repository.SaveAllAsync())
                {
                    return mapper.Map<ProductViewModel>(oldProduct);
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldProduct = await repository.GetProductAsync(id);
                if (oldProduct == null) return NotFound($"Cold not find product for that {id}");
                repository.Delete(oldProduct);
                if (await repository.SaveAllAsync())
		            return Ok();
            }
            catch (Exception ex)
            {
               
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

    }
}
