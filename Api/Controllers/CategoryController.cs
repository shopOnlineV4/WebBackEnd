using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {   
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //get All list category
                return Ok(await _unitOfWork.Categories.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok(await _unitOfWork.Categories.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody] CategoryMv category)
        {
            try
            {
                var res = _unitOfWork.Categories.CreateNewAddReturnObject(_mapper.Map<Category>(category));
                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), res);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CategoryMv categoryMv)
        {
            try
            {
                var category = _unitOfWork.Categories.GetById(id).Result;
                category.Name = categoryMv.Name;
                _unitOfWork.Categories.Edit(category);
                if (_unitOfWork.Commit()) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //check if product have implemet this cayegory then not allow
                var products = _unitOfWork.Products.Get(x => x.CategoryId == id).Result;
                if (products.Count() > 0) return BadRequest();

                _unitOfWork.Categories.Delete(id);
                if (_unitOfWork.Commit()) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }
    }
}
