using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace jairoEcomerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UloadController : ControllerBase
    {
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue
                        .Parse(file.ContentDisposition).FileName.Trim('"');
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(),
                        @"wwwroot\img",fileName);
                    var dbPath = Path.Combine("img", fileName);

                    using (var stream = new FileStream(pathToSave, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
