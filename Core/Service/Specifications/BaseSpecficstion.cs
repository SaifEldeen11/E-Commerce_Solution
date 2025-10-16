using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.Specifications
{
    public abstract class BaseSpecficstion<TEntity, TKey> : ISpecificstion<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecficstion(Expression<Func<TEntity,bool>>? criteria)
        {
                Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }


        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> OrderByExp)
        {
            OrderBy = OrderByExp;
        }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> OrderByDescExp)
        {
            OrderByDescending = OrderByDescExp;
        }
        #endregion

        #region Include
        public List<Expression<Func<TEntity, object>>> Includes { get; } = new List<Expression<Func<TEntity, object>>>();


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        } 
        #endregion

        #region Pagination
        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginged { get; set; }

        protected void ApplyPaging(int PageSize , int PageIndedx)
        {
            Take = PageSize;
            Skip = (PageIndedx - 1) * PageSize;
            IsPaginged = true;
        }
        #endregion
    }
}
