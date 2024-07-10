using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : EntityBase
    {
        public Expression<Func<T, bool>> Ceriateria { get; set; } = null;
        public List<Expression<Func<T, object>>>includes { get ; set; }=  new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDescending { get; set; } = null;
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPagination { get ; set ; }

        public BaseSpecification()
        {

        }
        public BaseSpecification(Expression<Func<T, bool>> ceriateria)
        {
            Ceriateria = ceriateria;
           
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderBy)
        {
            this.OrderBy = OrderBy;
        }

        public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescending)
        {
           this.OrderByDescending = OrderByDescending;
        }
        public void AddPagination(int skip,int take)
        {
            IsPagination= true;
            Skip= skip;
            Take= take;
        }
    }
}
