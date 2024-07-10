using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
        //    if (typeof(T)==typeof(Product))
        //       return (IEnumerable<T>) await _storeContext.Product.Include(p=>p.productType).Include(p => p.productBrand).ToListAsync();

            return await _storeContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetById(int id)
        {
           return await _storeContext.Set<T>().FindAsync(id);

        }


        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();

        }

        public async Task<T?> GetByIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();

        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvalutors<T>.GetQuery(_storeContext.Set<T>(), spec);
        }

        public async Task Add(T entity)
            =>await _storeContext.Set<T>().AddAsync(entity);
         

        public void update(T entity)
            => _storeContext.Set<T>().Update(entity);

        public void delete(T entity)
            => _storeContext.Set<T>().Remove(entity);

    }
}
