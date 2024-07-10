using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T:EntityBase
    {
        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<T?> GetById(int id);

        public Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

        public Task<T?> GetByIdWithSpec(ISpecification<T> spec);

        public Task<int> GetCountWithSpec(ISpecification<T> spec);
        Task Add(T entity);
        void update(T entity);  
        void delete(T entity);  


    }
}
