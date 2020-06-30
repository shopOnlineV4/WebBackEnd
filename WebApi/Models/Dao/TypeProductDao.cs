using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Entities;
using UnitOfWork;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class TypeProductDao : IFactory<TypeProductMv>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TypeProductDao(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;


        }
        
        public async Task<List<TypeProductMv>> GetAll()
        {
            var data = await _unitOfWork.TypeProducts.GetAll();
            return _mapper.Map<List<TypeProductMv>>(data.ToList());
        }

        public TypeProductMv GetById(object id)
        {
            return _mapper.Map<TypeProductMv>(_unitOfWork.TypeProducts.GetById(id).Result);
        }

        public TypeProductMv CreateNew(TypeProductMv data)
        {
            var product = _mapper.Map<TypeProduct>(data);
            product = _unitOfWork.TypeProducts.CreateNewAddReturnObject(product);
            return _unitOfWork.Commit() ? _mapper.Map<TypeProductMv>(product) : null;
        }

        public bool Update(object id, TypeProductMv data)
        {
            var typeProduct = _unitOfWork.TypeProducts.GetById(id).Result;
            typeProduct.ColorId = data.ColorId;
            typeProduct.SizeId = data.ColorId;
            return _unitOfWork.Commit();
        }

        public bool Delete(object id)
        {
            _unitOfWork.TypeProducts.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool Disable(object id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TypeProductMv>> GetAll(int index = 1, int take = 10)
        {
            throw new NotImplementedException();
        }
    }
}
