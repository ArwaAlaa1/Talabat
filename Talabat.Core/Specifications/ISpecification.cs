using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : EntityBase
    {
        public Expression<Func<T,bool>> Ceriateria { set;  get; } //for where operator

        public List<Expression<Func<T,object>>> includes { set; get; } //for include operator

        public Expression<Func<T,object>> OrderBy { set; get; }
        public Expression<Func<T, object>> OrderByDescending { set; get; }

        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }



    }
}
