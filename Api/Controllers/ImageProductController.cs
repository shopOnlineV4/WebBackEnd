using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Common;
using Api.Models.Collection;
using AutoMapper;
using Domain.Models.Entities;
using ModelViews.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ModelViews;
using UnitOfWork;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ImageProductController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        // GET: api/<ImageProduct>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var images = _mapper.Map<List<ImageMv>>(await _unitOfWork.Images.GetAll());
                return Ok(images);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No Data");
            }
        }

        // GET api/<ImageProduct>/5
        [HttpGet("GetByProductId/{id}")]
        public async Task<IActionResult> GetByProductId(Guid id)
        {
            try
            {

                var product = _mapper.Map<ProductMv>(await _unitOfWork.Products.GetById(id));
                var images = _mapper.Map<List<ImageMv>>(await _unitOfWork.Images.Get(x => x.ProductId == id));
                foreach (var item in images)
                {
                    item.UserCreate = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(item.CreateBy).Result);
                    item.UserModified = _mapper.Map<UserInfor>(_unitOfWork.AppUsers.GetById(item.ModifiedBy).Result);
                    item.ImageLocation = ConstString.BaseLocationImageLink + item.FileName;
                }
                product.Images = images;
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("No Data");
            }
        }
        // POST api/<ImageProduct>
        [HttpPost]
        public IActionResult Post([FromBody] ImageInputMv imageData)
        {

            try
            {
                var image = new Image();
                if (imageData.FileInput == null)
                {
                    image.FileName = "default.jpg";
                }
                else
                {
                    image.FileName = new SaveFile(_hostEnvironment).SaveB64File(imageData.FileInput);
                }
                image.Status = (int)Status.Active;
                image.CreateBy = imageData.CreateBy;
                image.DateCreate = DateTime.Now;
                image.ProductId = imageData.ProductId;
                var res = _unitOfWork.Images.CreateNewAddReturnObject(image);
                if (_unitOfWork.Commit()) return Created(Url.Action("Get"), _mapper.Map<ImageMv>(res));
                return BadRequest();
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
        }

        // PUT api/<ImageProduct>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ImageInputMv imageData)
        {
            try
            {
                var image = await _unitOfWork.Images.GetById(id);
                if (imageData.FileInput == null)
                {
                    image.FileName = "default.jpg";
                }
                else
                {
                    if (image.FileName != "default.jpg")
                    {
                        new SaveFile(_hostEnvironment).DeteFile(image.FileName);
                    }
                    image.FileName = new SaveFile(_hostEnvironment).SaveB64File(imageData.FileInput);
                }
                _unitOfWork.Images.Edit(image);

                if (_unitOfWork.Commit()) return Ok(image);
                return BadRequest("fail");

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        // DELETE api/<ImageProduct>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _unitOfWork.Images.Delete(id);
            if (_unitOfWork.Commit()) return Ok("Success");
            return NotFound("Fail!");
        }
    }
}
