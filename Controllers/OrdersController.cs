using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jairoEcomerce.Data;
using jairoEcomerce.Data.Entities;
using jairoEcomerce.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jairoEcomerce.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public OrdersController(IEcommerceRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper; 
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = repository.GetOrders();

                return Ok(mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
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

                //if (order != null) return Ok(order);
                if (order != null) return Ok(mapper.Map<Order,OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders: {ex}");
                return BadRequest("Failed to get orders");
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = mapper.Map<OrderViewModel,Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }
                    repository.AddOrder(newOrder);
                    if (repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}",
                            mapper.Map<Order, OrderViewModel>(newOrder));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Failed to save a new order: {ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}
