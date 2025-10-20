using Domain.Contracts;
using Domain.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence.Repostiries
{
    public class BasketRepostiry(IConnectionMultiplexer connection) : IBasketRepostiry
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<Basket?> CreateOrUpdateBasketAsync(Basket basket, TimeSpan? timeoLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);

            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, jsonBasket, timeoLive ?? TimeSpan.FromDays(3));

            if(isCreatedOrUpdated)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<Basket?> GetBasketAsync(string key)
        {
            var basket =await _database.StringGetAsync(key);

            if(basket.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<Basket>(basket!);

        }
    }
}
