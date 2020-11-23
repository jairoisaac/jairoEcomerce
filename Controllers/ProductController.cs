using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jairoEcomerce.Data;
using jairoEcomerce.Data.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jairoEcomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IEcommerceRepository repository;

        public ProductController(IEcommerceRepository repository)
        {
            this.repository = repository;
        }    
        // GET: api/<ProductController>
        [HttpGet] 
        public IEnumerable<Product> Get()
        {
            var myProduct = repository.GetProducts().ToList();
            return myProduct;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
