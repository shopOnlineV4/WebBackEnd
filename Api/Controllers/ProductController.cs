using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Api.Common;
using Api.Models.Collection;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using ModelViews.Enum;
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
        public async Task<IActionResult> Get(string name = "")
        {
            try
            {
                //get All list Product
                var products = await _unitOfWork.Products.Get(x => x.Name.Contains(name));
                var data = _mapper.Map<List<ProductForList>>(products);
                // using auto mapper to auto mapping modelView  if same fieldName
                foreach (var product in data)
                {
                    product.Category = _mapper.Map<CategoryInfo>(_unitOfWork.Categories.GetById(product.CategoryId).Result);
                    product.UserCreate = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.CreateBy).Result);
                    product.UserModified = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.ModifiedBy).Result);
                    product.ImageProductLocation = ConstString.BaseLocationImageLink + product.ProductImage;
                }
                return Ok(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }

        }
        [HttpGet]
        [Route("ProductPaging")]
        public async Task<IActionResult> ProductPaging(int index = 1, int size = 10, string name = "")
        {

            //select and paging by template<Product> 
            var products = await _unitOfWork.Products.Get(x => x.Name.Contains(name));
            try
            {
                var page = new PagingModelView<ProductForList>
                {
                    index = index,
                    Size = size,
                    TotalCount = products.Count(),
                    TotalPages = (products.Count() / size)
                };
                //get All list Product
                var data = _mapper.Map<List<ProductForList>>(products);
                foreach (var product in data)
                {
                    product.Category = _mapper.Map<CategoryInfo>(_unitOfWork.Categories.GetById(product.CategoryId).Result);
                    product.UserCreate = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.CreateBy).Result);
                    product.UserModified = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.ModifiedBy).Result);
                    product.ImageProductLocation = ConstString.BaseLocationImageLink + product.ProductImage;
                }
                page.Items = data;
                return Ok(page);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }

        }
        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {   
                var product = _mapper.Map<ProductMv>(await _unitOfWork.Products.GetById(id));
                product.Category = _mapper.Map<CategoryInfo>(_unitOfWork.Categories.GetById(product.CategoryId).Result);
                product.UserCreate = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.CreateBy).Result);
                product.UserModified = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(product.ModifiedBy).Result);
                product.ImageProductLocation = ConstString.BaseLocationImageLink + product.ProductImage;
               
                product.TypeProducts = null;
                product.Images = null;
               
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("No data");
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] InsertProduct data)
        {
            var product = new Product();
            product.Name = data.Name;
            product.CategoryId = data.CategoryId;
            product.Price = data.Price;
            product.Detail = data.Detail;
            product.Status = (int)Status.Active;
            product.CreateBy = data.CreateBy;
            product.DateCreate = DateTime.Now;
            if (data.FileData == null)
            {
                product.ProductImage = "default.jpg";
            }
            else
            {
                product.ProductImage = new SaveFile(_hostEnvironment).SaveB64File(data.FileData);
            }
            product = _unitOfWork.Products.CreateNewAddReturnObject(product);
            if (_unitOfWork.Commit()) return Created(Url.Action("Get"), _mapper.Map<ProductMv>(product));
            return BadRequest();
        }
        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] InsertProduct data)
        {
            try
            {
                var product = await _unitOfWork.Products.GetById(id);
                product.Name = data.Name;
                product.CategoryId = data.CategoryId;
                product.Price = data.Price;
                product.Status = (int)Status.Active;
                product.ModifiedBy = data.ModifiedBy;
                product.Detail = data.Detail;
                product.DateModified = DateTime.Now;
                if (data.FileData == null)
                {
                    product.ProductImage = "default.jpg";
                }
                else
                {
                    if (product.ProductImage != "default.jpg")
                    {
                        new SaveFile(_hostEnvironment).DeteFile(product.ProductImage);
                    }
                    product.ProductImage = new SaveFile(_hostEnvironment).SaveB64File(data.FileData);
                }
                _unitOfWork.Products.Edit(product);
                if (_unitOfWork.Commit()) return Ok(_mapper.Map<ProductMv>(product));
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _unitOfWork.Products.Delete(id);
            if (_unitOfWork.Commit()) return Ok("Success");
            return NotFound("Fail!");
        }
    }
}
