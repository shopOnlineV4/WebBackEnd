using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork;
using WebApi.Models.Dao;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
       private readonly IContainer _container;

        public ProductController(IContainer container)
        {
            _container = container;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_container.ProductFactory.GetAll().Result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_container.ProductFactory.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // POST: api/Product
        [HttpPost]
        public IActionResult Post([FromBody] ProductMv product)
        {
            try
            {
                return Created(Url.Action("Get"), _container.ProductFactory.CreateNew(product));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] ProductMv product)
        {
            try
            {
                return Ok(_container.ProductFactory.Update(id,product));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }


        // : api/ApiWithActions/5
        [HttpGet("Disable/{id}")]
        public IActionResult Disable(Guid id)
        {
            try
            {
                if (_container.ProductFactory.Disable(id)) return Ok();
                return NoContent();
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
                if (_container.ProductFactory.Delete(id)) return Ok();
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
