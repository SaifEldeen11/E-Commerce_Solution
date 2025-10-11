﻿using AutoMapper;
using Domain.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public class ServiceManger(IUnitOfWork _unitOfWork,IMapper _mapper) : IServiceManger
    {

        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(()=> new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProductService.Value;
    }
}
