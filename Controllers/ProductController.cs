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
    public class ProductController : Controller
    {
        private readonly IEcommerceRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;

        public ProductController(IEcommerceRepository repository, ILogger<OrdersController> logger,
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
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
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            // USING PRODUCT ID = 28 TO TEST
             try
            {
                Product myProduct = repository.GetProduct(id);
                string Name = myProduct.ImageUrl;
                char[] charsToTrim = { '*', ' ', '\'' };
                Name = Name.Trim(charsToTrim);
                // Get data from the database from id,
                // The name comes from a field imageURL, no from a paramenter of the function.
                using System.Drawing.Image image = System.Drawing.Image.FromFile("C:\\Papi\\AngularNET\\JairoEComerce\\jairoEcomerce\\Resources\\Images\\"+Name+".JPG");
                using MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, image.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                String base64String = Convert.ToBase64String(imageBytes);
                myProduct.ImageUrl = base64String;
                //var image = System.IO.File.OpenRead("C:\\Papi\\AngularNET\\JairoEComerce\\jairoEcomerce\\Resources\\Images\\"+Name+".JPG");
                // Turn that image into a strding.
                // save the string into the appropriate object variable
                //return await Task.Run(() => File(image, "image/jpeg"));
                return Ok(myProduct);
            }
            catch (Exception)
            {

                throw;
            }
        }


        //public IActionResult Post([FromBody] Product model)
        [HttpPost]
        public ActionResult<ProductViewModel> Post([FromBody]ProductViewModel model)
        //public ActionResult<Product> Post(Product model)
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
