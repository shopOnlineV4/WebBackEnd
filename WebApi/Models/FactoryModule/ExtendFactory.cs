using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UnitOfWork;

namespace WebApi.Models.FactoryModule
{
    public abstract class ExtendFactory<T,TMv>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        protected ExtendFactory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
    }
}
