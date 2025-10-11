using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepostiry<TEntity,TKey> GetRepostiry<TEntity,TKey>() where TEntity : BaseEntity<TKey>;

        Task<int> SaveChangesAsync();


    }
}
