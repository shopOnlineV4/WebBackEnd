using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UnitOfWork
{
    public interface IRepository<T> where T : class
    {
       Task<IEnumerable<T>> GetAll();

        public Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        Task<T> GetById(object id);

        void CreateNew(T t);

        T CreateNewAddReturnObject(T t);

        void Edit(T t);

        void Delete(object id);
    }
}