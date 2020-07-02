using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Collection;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                //get All list Product
                return Ok(await _unitOfWork.Products.GetAll());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
           
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _unitOfWork.Products.GetById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductMv data)
        {
            if (data.FileData == null)
            {
                data.ProductImage = "default.jpg";
            }
            else
            {
                data.ProductImage = SaveFile.SaveB64File(data.FileData);
            }

            var product = _mapper.Map<Product>(data);
            product = _unitOfWork.Products.CreateNewAddReturnObject(product);
            if (_unitOfWork.Commit()) return Ok(_mapper.Map<ProductMv>(product));
            return BadRequest();          
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
