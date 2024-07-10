using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext storeContext;
        private Hashtable _Repositories;
        public UnitOfWork(StoreContext storeContext)
        {
            this.storeContext = storeContext;
            _Repositories=new Hashtable();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase
        {
            var type=typeof(TEntity).Name;
            if (!_Repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(storeContext);
                _Repositories.Add(type, repository);

            }
            return _Repositories[type] as IGenericRepository<TEntity>;
        }
        public async Task<int> Complet()
         => await storeContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
         => await  storeContext.DisposeAsync();
        
    }
}
