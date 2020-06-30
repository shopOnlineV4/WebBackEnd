using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProductController : ControllerBase
    {
        private readonly IContainer _container;

        public ImageProductController(IContainer container)
        {
            _container = container;
        }

        // GET: api/ImageProduct
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ImageProduct/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/ImageProduct/5
        [HttpGet("GetByProductId/{id}")]
        public IActionResult GetByProductId(Guid id)
        {
            try
            {
                var product = _container.ProductFactory.GetById(id);
                product.Images = _container.ImageFactory.GetAll().Result.Where(x => x.ProductId == id).ToList();
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }


        // POST: api/ImageProduct
        [HttpPost]
        public IActionResult Post([FromBody] ImageMv image )
        {
            try
            {
                return Created(Url.Action("Get"), _container.ImageFactory.CreateNew(image));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (_container.ImageFactory.Delete(id)) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest();
            }
        }
    }
}
