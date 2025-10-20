using Domain.Models.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IBasketRepostiry
    {
        Task<Basket?> GetBasketAsync(string key);

        Task<Basket?> CreateOrUpdateBasketAsync(Basket basket,TimeSpan? timeoLive = null);

        Task<bool> DeleteBasketAsync(string key);




    }
}
