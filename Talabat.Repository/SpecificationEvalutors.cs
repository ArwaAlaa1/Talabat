using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static  class SpecificationEvalutors<T> where T : EntityBase
    {
        public static IQueryable<T> GetQuery(IQueryable<T> Inputquery , ISpecification<T> spec)
        {
            var query = Inputquery;

            if (spec.Ceriateria is not null)
                query = query.Where(spec.Ceriateria);


            if (spec.OrderBy is not null)
                query=query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending is not null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagination)
                query = query.Skip(spec.Skip).Take(spec.Take);

            query = spec.includes.Aggregate(query, (currentQuery, NextQuery) => currentQuery.Include(NextQuery));
            return query;
        }
    }
}
