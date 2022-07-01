using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntiry> where TEntiry : BaseEntity
    {
        public static IQueryable<TEntiry> GetQuery(IQueryable<TEntiry> inputQuary, ISpecification<TEntiry> spec)
        {
            var quary = inputQuary;

            if(spec.Criteria != null)
            {
                quary = quary.Where(spec.Criteria);
            }

            if(spec.OrderBy != null)
            {
                quary = quary.OrderBy(spec.OrderBy);
            }

            if(spec.OrderByDescending != null)
            {
                quary = quary.OrderByDescending(spec.OrderByDescending);
            }

            if(spec.IsPagingEnabled)
            {
                quary = quary.Skip(spec.Skip).Take(spec.Take);
            }

            quary = spec.Includes.Aggregate(quary, (current, include) => current.Include(include));

            return quary;
        }
    }
}