using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UnitOfWork;
using WebApi.Models.Dao;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IContainer _container;

        public CategoryController(IContainer container)
        {
            _container = container;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _container.CategoryFactory.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                return Ok(_container.CategoryFactory.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Post([FromBody] CategoryMv category)
        {
            try
            {
                return Created(Url.Action("Get"), _container.CategoryFactory.CreateNew(category));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CategoryMv category)
        {
            try
            {
                if (_container.CategoryFactory.Update(id,category)) return Ok();
                return BadRequest();
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
                if (_container.CategoryFactory.Delete(id)) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}