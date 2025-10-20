using AutoMapper;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models.BasketModule;
using ServiceAbstraction;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public class BasketService(IBasketRepostiry _basketRepostiry,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateAsync(BasketDto basket)
        {
            var basketModel = _mapper.Map<BasketDto,Basket>(basket);
            var createOrUpdateBasket = await _basketRepostiry.CreateOrUpdateBasketAsync(basketModel);
            if(createOrUpdateBasket is not null)
            {
                return await GetBasketAsync(basket.Id);
            }
            throw new Exception("Failed to create or update basket");



        }

        public async Task<bool> DeleteBasketAsync(string key)
        {
            return await _basketRepostiry.DeleteBasketAsync(key);
        }

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket =  await _basketRepostiry.GetBasketAsync(key);
            if(basket is not null)
            {
                return _mapper.Map<Basket,BasketDto>(basket);
            }
            throw new BasketNotFoundExeption(key);
        }
    }
}
