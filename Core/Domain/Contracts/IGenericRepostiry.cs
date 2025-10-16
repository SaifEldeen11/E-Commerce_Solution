using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepostiry<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecificstion<TEntity,TKey> specificstion);

        Task<TEntity?> GetByIdAsync(ISpecificstion<TEntity,TKey> specificstion);

        Task<int> CountAsync(ISpecificstion<TEntity, TKey> specificstion);


    }
}
