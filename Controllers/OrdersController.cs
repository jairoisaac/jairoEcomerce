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
    public class OrdersController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IEcommerceRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = repository.GetOrders();

                return Ok(results);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }
        
        
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = repository.GetOrderById(id);

                if (order != null) return Ok(order);
                else return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]Order model)
        {
            try
            {
                repository.AddEntity(model);
                if (repository.SaveAll())
                {
                    return Created($"/api/orders/{model.Id}", model);
                }
                
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to save a new order: {ex}");
            }
            return BadRequest("Failed to save new order: {ex}");
        }
    }
}
