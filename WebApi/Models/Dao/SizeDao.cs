using AutoMapper;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class SizeDao : IFactory<SizeMv>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SizeDao(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public SizeMv CreateNew(SizeMv data)
        {
            var value = _unitOfWork.Sizes.CreateNewAddReturnObject(_mapper.Map<Size>(data));
            return _unitOfWork.Commit() ? _mapper.Map<SizeMv>(value) : null;
        }

        public bool Delete(object id)
        {
            if (_unitOfWork.TypeProducts.Get(x => x.SizeId == Convert.ToInt32(id)).Result.Count() > 0)
                return false;
            _unitOfWork.Sizes.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool Disable(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SizeMv>> GetAll()
        {
            return _mapper.Map<List<SizeMv>>(await _unitOfWork.Sizes.GetAll());
        }

        public Task<List<SizeMv>> GetAll(int index = 1, int take = 10)
        {
            throw new NotImplementedException();
        }

        public SizeMv GetById(object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(object id, SizeMv data)
        {
            throw new NotImplementedException();
        }
    }
}
