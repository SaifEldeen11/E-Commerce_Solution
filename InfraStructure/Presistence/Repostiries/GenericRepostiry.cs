using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presentaion;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repostiries
{
    public class GenericRepostiry<TEntity, TKey>(StoreDbContext _dbContext) :IGenericRepostiry<TEntity ,TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<int> CountAsync(ISpecificstion<TEntity, TKey> specificstion)
        {
            return await SpecificationsEvalutor.CreateQuery(_dbContext.Set<TEntity>(), specificstion).CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecificstion<TEntity, TKey> specificstion)
        {
            return await SpecificationsEvalutor.CreateQuery(_dbContext.Set<TEntity>(),specificstion).ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(ISpecificstion<TEntity, TKey> specificstion)
        {
            return await SpecificationsEvalutor.CreateQuery(_dbContext.Set<TEntity>(), specificstion).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id).AsTask();
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
