using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using UnitOfWork;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class CategoryDao : IFactory<CategoryMv>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryDao(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public virtual async Task<List<CategoryMv>> GetAll()
        {
            var data = await _unitOfWork.Categories.GetAll();
            var categories = _mapper.Map<List<CategoryMv>>(data.ToList());
            foreach (var category in categories)
            {
                category.CategoryDataParent = categories.SingleOrDefault(x => x.Id == category.SubCategoryId);
                category.Products = null;
            }
            return categories;
        }

        public virtual CategoryMv GetById(object id)
        {
            var value = _mapper.Map<CategoryMv>(_unitOfWork.Categories.GetById(id).Result);
            value.ListChilds = GetAll().Result.Where(x => x.SubCategoryId == value.Id).ToList();
            return value;
        }

        public virtual CategoryMv CreateNew(CategoryMv data)
        {
            var category = _mapper.Map<Category>(data);
            category = _unitOfWork.Categories.CreateNewAddReturnObject(category);
            return _unitOfWork.Commit() ? _mapper.Map<CategoryMv>(category) : null;
        }

        public bool Update(object id, CategoryMv data)
        {
            var category = _unitOfWork.Categories.GetById(id).Result;
            category.Name = data.Name;
            return _unitOfWork.Commit();
        }

        public virtual bool Delete(object id)
        {
            _unitOfWork.Categories.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool Disable(object id)
        {
            return false;
        }

        public Task<List<CategoryMv>> GetAll(int index = 1, int take = 10)
        {
            throw new NotImplementedException();
        }
    }
}
