using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecificstion<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Expression that defines the criteria for the specification
        public Expression<Func<TEntity,bool>>? Criteria { get;}

        public List<Expression<Func<TEntity,object>>> Includes { get; }

        Expression<Func<TEntity,object>> OrderBy { get; }
        Expression<Func<TEntity,object>> OrderByDescending { get; }

        public int Take { get;}
        public int Skip { get;}

        public bool IsPaginged{ get; }
    }
}
