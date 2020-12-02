using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using jairoEcomerce.Data;
using jairoEcomerce.Data.Entities;
using jairoEcomerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace jairoEcomerce.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IEcommerceRepository repository,
          ILogger<OrderItemsController> logger,
          IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpGet] 
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null)
                return Ok(mapper.Map<IEnumerable<OrderItem>,
                    IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if (item != null)
                {
                    return Ok(mapper.Map<OrderItem,OrderItemViewModel> (item));
                }
            }
            return NotFound();

        }
    }
}
