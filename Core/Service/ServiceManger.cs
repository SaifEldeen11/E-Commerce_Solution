using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepostiry _basketRepostiry,UserManager<ApplicationUser> _userManager,IConfiguration _configuration) : IServiceManger
    {

        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(()=> new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService = new Lazy<IBasketService>(() => new BasketService(_basketRepostiry, _mapper));
        private readonly Lazy<IAuthenticationServices> _authenticationServices = new Lazy<IAuthenticationServices>(() => new AuthenticationService(_userManager, _configuration,_mapper));
        private readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepostiry, _unitOfWork));
        public IProductService ProductService => _LazyProductService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationServices AuthenticationServices => _authenticationServices.Value;

        public IOrderService OrderService => _LazyOrderService.Value;
    }
}
