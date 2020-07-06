using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TypeProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TypeProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        // GET api/<TypeProductController>/5
        [HttpGet("GetTypeProductByProductId/{id}")]
        public async Task<IActionResult> GetTypeProductByProductId(Guid id)
        {
            try
            {
                var product = _mapper.Map<ProductMv>(await _unitOfWork.Products.GetById(id));
                product.Category = _mapper.Map<CategoryInfo>(_unitOfWork.Categories.GetById(product.CategoryId).Result);
                product.TypeProducts = _mapper.Map<List<TypeProductMv>>(_unitOfWork.TypeProducts.Get(x => x.ProductId == id).Result.ToList());
                foreach (var item in product.TypeProducts)
                {
                    item.ColorCode = _mapper.Map<ColorCodeMv>(await _unitOfWork.ColorCodes.GetById(item.ColorId));
                    item.Size = _mapper.Map<SizeMv>(await _unitOfWork.Sizes.GetById(item.SizeId));
                }
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // POST api/<TypeProductController>
        [HttpPost]
        public IActionResult Post([FromBody] TypeProductInput typeProductInput)
        {
            try
            {
                var typeProduct = new TypeProduct()
                {
                    ColorId = typeProductInput.ColorId,
                    ProductId = typeProductInput.ProductId,
                    SizeId = typeProductInput.SizeId
                };
                typeProduct = _unitOfWork.TypeProducts.CreateNewAddReturnObject(typeProduct);
                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), _mapper.Map<TypeProductMv>(typeProduct));
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT api/<TypeProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TypeProductInput typeProductInput)
        {
            try
            {
                var typeProduct = await _unitOfWork.TypeProducts.GetById(id);
                if (typeProduct == null) return NotFound("null");
                typeProduct.ColorId = typeProductInput.ColorId;
                typeProduct.ProductId = typeProductInput.ProductId;
                typeProduct.SizeId = typeProductInput.SizeId;

                _unitOfWork.TypeProducts.Edit(typeProduct);
                if (_unitOfWork.Commit()) return Ok();
                return BadRequest("fail");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<TypeProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _unitOfWork.TypeProducts.Delete(id);
            if (_unitOfWork.Commit())
                return Ok("Success");
            return NotFound("Fail!");
        }


        //COLOR MANAGE


        //GET : api/<TypeProductController>/GetListColor
        [HttpGet("Color")]
        public async Task<IActionResult> GetListColor()
        {
            var color = await _unitOfWork.ColorCodes.GetAll();
            return Ok(color);
        }

        //GET : api/<TypeProductController>/GetListColor
        [HttpGet("Color/{id}")]
        public async Task<IActionResult> GetColorById(int id)
        {
            var color = await _unitOfWork.ColorCodes.GetById(id);
            if (color == null) return NotFound("null");
            return Ok(color);
        }

        // POST api/<TypeProductController>/Color
        [HttpPost]
        [Route("Color")]
        public IActionResult AddColor([FromBody] ColorInput colorInput)
        {
            try
            {
                var color = new ColorCode()
                {
                    ColorData = colorInput.ColorData,
                    Name = colorInput.Name
                };
                var res = _unitOfWork.ColorCodes.CreateNewAddReturnObject(color);
                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), res);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }


        // PUT api/<TypeProductController>/Color
        [HttpPut]
        [Route("Color")]
        public async Task<IActionResult> EditColor(int id, [FromBody] ColorInput colorInput)
        {
            try
            {
                var data = await _unitOfWork.ColorCodes.GetById(id);
                if (data == null) return NotFound("nulls");
                data.Name = colorInput.Name;
                data.ColorData = colorInput.ColorData;
                _unitOfWork.ColorCodes.Edit(data);
                if (_unitOfWork.Commit()) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // DELETE api/<TypeProductController>/Color/5
        [HttpDelete]
        [Route("Color/{id}")]
        public IActionResult DeleteColor(int id)
        {
            try
            {
                _unitOfWork.ColorCodes.Delete(id);
                if (_unitOfWork.Commit()) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        //SIZE MANAGEMENT

        //GET : api/<TypeProductController>/Size
        [HttpGet("Size")]
        public async Task<IActionResult> GetListSize()
        {
            var size = await _unitOfWork.Sizes.GetAll();
            return Ok(size);
        }

        //GET : api/<TypeProductController>/GetListColor
        [HttpGet("Size/{id}")]
        public async Task<IActionResult> GetSizeById(int id)
        {
            var color = await _unitOfWork.Sizes.GetById(id);
            if (color == null) return NotFound("null");
            return Ok(color);
        }

        // POST api/<TypeProductController>/size
        [HttpPost]
        [Route("Size")]
        public IActionResult AddSize([FromBody] SizeInput size)
        {
            try
            {
                var sizeInput = _unitOfWork.Sizes.CreateNewAddReturnObject(new Size()
                {
                    Name = size.Name
                });
                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), sizeInput);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        // PUT api/<TypeProductController>/size
        [HttpPut]
        [Route("Size")]
        public async Task<IActionResult> EditSize(int id, [FromBody] SizeInput sizeInput)
        {
            try
            {
                var data = await _unitOfWork.Sizes.GetById(id);
                if (data == null) return NotFound("null");

                data.Name = sizeInput.Name;

                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), sizeInput);
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        // DELETE api/<TypeProductController>/Color/5
        [HttpDelete]
        [Route("Size/{id}")]
        public IActionResult DeleteSize(int id)
        {
            try
            {
                _unitOfWork.Sizes.Delete(id);
                if (_unitOfWork.Commit()) return Ok();
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