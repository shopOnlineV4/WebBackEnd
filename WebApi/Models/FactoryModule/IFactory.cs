using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.FactoryModule{
    public interface IFactory<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<List<T>> GetAll(int index = 1, int take = 10);
        T GetById(object id);
        T CreateNew(T data);
        bool Update(object id, T data);
        bool Delete(object id);
        bool Disable(object id);
    }

}
