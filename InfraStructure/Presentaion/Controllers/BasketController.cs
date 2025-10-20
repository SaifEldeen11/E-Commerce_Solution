using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManger _serviceManger):ControllerBase
    {
        // Get Basket
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManger.BasketService.GetBasketAsync(key);
            return Ok(basket);
        }

        // Create or update basket
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket)
        {
            var Basket = await _serviceManger.BasketService.CreateOrUpdateAsync(basket);
            return Ok(basket);
        }

        // Delete basket
        [HttpDelete("{key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await _serviceManger.BasketService.DeleteBasketAsync(key);
            return Ok(result);
        }




    }
}
