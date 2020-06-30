using AutoMapper;
using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class ColorCodeDao : IFactory<ColorCodeMv>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ColorCodeDao(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        public ColorCodeMv CreateNew(ColorCodeMv data)
        {
            var value = _unitOfWork.ColorCodes.CreateNewAddReturnObject(_mapper.Map<ColorCode>(data));
            return _unitOfWork.Commit() ? _mapper.Map<ColorCodeMv>(value) : null;
        }

        public bool Delete(object id)
        {
            if (_unitOfWork.TypeProducts.Get(x => x.ColorId == Convert.ToInt32(id)).Result.Count() > 0)
                return false;
            _unitOfWork.ColorCodes.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool Disable(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ColorCodeMv>> GetAll()
        {
            return _mapper.Map<List<ColorCodeMv>>(await _unitOfWork.ColorCodes.GetAll());
        }

        public Task<List<ColorCodeMv>> GetAll(int index = 1, int take = 10)
        {
            throw new NotImplementedException();
        }

        public ColorCodeMv GetById(object id)
        {
            throw new NotImplementedException();
        }

        public bool Update(object id, ColorCodeMv data)
        {
            throw new NotImplementedException();
        }
    }
}
