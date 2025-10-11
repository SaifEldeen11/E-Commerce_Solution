using Domain.Contracts;
using Domain.Models;
using Presistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repostiries
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string,object> _repostires = new Dictionary<string, object>();
        public IGenericRepostiry<TEntity, TKey> GetRepostiry<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repostires.ContainsKey("Product"))
            {
                return (IGenericRepostiry<TEntity, TKey>) _repostires[typeName];
            }
            // Create rep Object
            var repo = new GenericRepostiry<TEntity, TKey>(_dbContext);

            // Store obj reference in dictionary
            _repostires[typeName] = repo;

            // return object 
            return repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }
    }
}
