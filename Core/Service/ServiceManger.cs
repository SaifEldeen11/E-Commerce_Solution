using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepostiry _basketRepostiry) : IServiceManger
    {

        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(()=> new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepostiry, _mapper));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _basketService.Value;
    }
}
