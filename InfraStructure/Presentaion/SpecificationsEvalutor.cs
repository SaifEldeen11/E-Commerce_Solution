using Domain.Contracts;
using Domain.Models;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;   
using System.Collections.Generic;

namespace Presentaion
{
    public static class SpecificationsEvalutor
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> InputQuery, ISpecificstion<TEntity,TKey> specificstion) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;

            if (specificstion.Criteria is not null)
            {
                query.Where(specificstion.Criteria);
            }
            if(specificstion.OrderBy is not null)
            {
                query = query.OrderBy(specificstion.OrderBy);
            }
            if(specificstion.IsPaginged)
            {
                query= query.Skip(specificstion.Skip);
                query= query.Take(specificstion.Take);
            }


            if(specificstion.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specificstion.OrderByDescending);
            }

            if (specificstion.Includes is not null && specificstion.Includes.Any())
            {
                foreach (var expression in specificstion.Includes)
                {
                    query = query.Include(expression);
                }

                // another way

                //query = specificstion.Includes.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }




            return query;
        }
    }
}
