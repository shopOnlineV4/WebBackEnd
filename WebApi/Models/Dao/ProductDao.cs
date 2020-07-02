using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using Domain.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using UnitOfWork;
using WebApi.Common;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class ProductDao : IFactory<ProductMv>
    {


        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductDao(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<List<ProductMv>> GetAll()
        {
            var data = _mapper.Map<List<ProductMv>>(await _unitOfWork.Products.GetAll());
            foreach (var product in data)
            {
                product.ImageProductLocation = ConstString.BaseLocationImageLink + product.ProductImage;
                product.Category = _mapper.Map<CategoryMv>(_unitOfWork.Categories.GetById(product.CategoryId).Result);
                product.Category.Products = null;
            }
            return data.ToList();
        }
        public ProductMv GetById(object id)
        {
            var data = _mapper.Map<ProductMv>(_unitOfWork.Products.GetById(id).Result);
            data.ImageProductLocation = ConstString.BaseLocationImageLink + data.ProductImage;
            return data;
        }
        public ProductMv CreateNew(ProductMv data)
        {
            if (data.FileData == null)
            {
                data.ProductImage = "default.jpg";
            }
            else
            {
                string fileName = DateTime.Now.Ticks + ".jpg";
                string pathBase = _hostEnvironment.WebRootPath + "\\productImages\\";
                if (!Directory.Exists(pathBase + "\\productImages\\"))
                    Directory.CreateDirectory(pathBase + "\\productImages\\");
                byte[] contents = Convert.FromBase64String(data.FileData);
                MemoryStream ms = new MemoryStream(contents, 0, contents.Length);
                File.WriteAllBytes(pathBase + fileName, contents);
                data.ProductImage = fileName;
            }

            var product = _mapper.Map<Product>(data);
            product = _unitOfWork.Products.CreateNewAddReturnObject(product);
            return _unitOfWork.Commit() ? _mapper.Map<ProductMv>(product) : null;
        }
        public bool Update(object id, ProductMv data)
        {
            string pathBase = _hostEnvironment.WebRootPath + "\\productImages\\";
            //check image have exist or defalut to delete

            if (data.FileData == null)
            {
                data.ProductImage = "default.jpg";
            }
            else
            {
                var productData = _unitOfWork.Products.GetById(id).Result;
                if (productData.ProductImage != "default.jpg")
                {
                    if (File.Exists(Path.Combine(pathBase, productData.ProductImage)))
                        File.Delete(Path.Combine(pathBase, productData.ProductImage));
                }
                string fileName = DateTime.Now.Ticks + ".jpg";

                if (!Directory.Exists(pathBase + "\\productImages\\"))
                    Directory.CreateDirectory(pathBase + "\\productImages\\");
                byte[] contents = Convert.FromBase64String(data.FileData);
                MemoryStream ms = new MemoryStream(contents, 0, contents.Length);
                File.WriteAllBytes(pathBase + fileName, contents);
                data.ProductImage = fileName;
            }


            var product = _unitOfWork.Products.GetById(id).Result;
            product.CategoryId = data.CategoryId;
            product.ModifiedBy = data.ModifiedBy;
            product.DateModified = DateTime.Now;
            product.Detail = data.Detail;
            product.Price = data.Price;
            product.Name = data.Name;
            product.ProductImage = data.ProductImage;
            _unitOfWork.Products.Edit(product);
            return _unitOfWork.Commit();
        }

        public bool Disable(object id)
        {
            var product = _unitOfWork.Products.GetById(id).Result;
            if (product.Status == 1) product.Status = (int)Status.Lock;
            else product.Status = (int)Status.Active;
            return _unitOfWork.Commit();

        }
        public bool Delete(object id)
        {
        //    if (_unitOfWork.Products
        //        .GetAll().Result
        //        .Count(x => x.Images.Any(image => image.ProductId == (Guid)id) ||
        //                    x.TypeProducts.Any(x => x.ProductId == (Guid)id)) > 0) return false;
        //    _unitOfWork.TypeProducts.Delete(id);
        //    return _unitOfWork.Commit();
        return false;
    }

    public Task<List<ProductMv>> GetAll(int index = 1, int take = 10)
    {
        throw new NotImplementedException();
    }
}
}
