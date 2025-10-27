using AutoMapper;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos.IdentityModule;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.MappingProfiles
{
    internal class OrderProfile :Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dist => dist.DeliveryMethod, options => options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dist => dist.ProductName, options => options.MapFrom(src => src.Product.ProductName))
                .ForMember(dist => dist.PictureUrl, options => options.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();


        }
    }
}
