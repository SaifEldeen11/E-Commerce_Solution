using AutoMapper;
using Domain.Models.BasketModule;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();

            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
