using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using UnitOfWork;
using WebApi.Models.Dao;
using WebApi.Models.ModelView;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebApi.Models.FactoryModule
{
    public class Container : IContainer
    {
        private IFactory<CategoryMv> _categoryFactory;
        private IFactory<ProductMv> _productFactory;
        private IFactory<TypeProductMv> _typeProductFactory;
        private IFactory<ImageMv> _ImageFactory;
        private IFactory<ColorCodeMv> _ColorCodeFactory;
        private IFactory<SizeMv> _SizeFactory;

      
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        
        public Container(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostingEnvironment;
        }
        public IFactory<CategoryMv> CategoryFactory => _categoryFactory ??= new CategoryDao(_unitOfWork, _mapper);

        public IFactory<ProductMv> ProductFactory => _productFactory ??= new ProductDao(_unitOfWork, _mapper, _hostEnvironment);

        public IFactory<TypeProductMv> TypeProductFactory => _typeProductFactory ??= new TypeProductDao(_unitOfWork, _mapper);

        [Obsolete]
        public IFactory<ImageMv> ImageFactory => _ImageFactory ??= new ImageProductDao(_unitOfWork, _mapper, _hostEnvironment);

        public IFactory<ColorCodeMv> ColorCodeFactory => _ColorCodeFactory ??= new ColorCodeDao(_unitOfWork, _mapper);

        public IFactory<SizeMv> SizeFactory => _SizeFactory ??= new SizeDao(_unitOfWork, _mapper);
    }
}
