using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using ModelViews;

namespace Api.Models.MappingProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AppUser, UserInfor>();
            CreateMap<Category, CategoryInfo>();
            CreateMap<Product, ProductForList>(); 
            CreateMap<Product, ProductMv>();
            CreateMap<Image, ImageMv>();

            CreateMap<TypeProduct, TypeProductMv>();
            CreateMap<ColorCode, ColorCodeMv>();
            CreateMap<Size, SizeMv>();


        }
    }
}
