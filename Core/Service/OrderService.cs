using Microsoft.EntityFrameworkCore.Metadata;
using ServiceAbstraction;
using AutoMapper;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.IdentityModule;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models;
using ServiceImplemntation.Specifications;

namespace ServiceImplemntation
{
    public class OrderService(IMapper _mapper,IBasketRepostiry _basketRepostiry,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            // map from address dto to Order Address
            var orderAdrress = _mapper.Map<AddressDto, OrderAddress>(orderDto.Address);

            var basket = await _basketRepostiry.GetBasketAsync(orderDto.BasketId);

            if (basket is null)
                throw new BasketNotFoundExeption(orderDto.BasketId);

            // Create Order Item List
            List<OrderItem> orderItems = [];
            var productRepo = _unitOfWork.GetRepostiry<Product,int>();
            foreach (var item in basket.Items)
            {
                var product = await productRepo.GetByIdAsync(item.Id);
                if (product is null)
                    throw new ProductNotFoundExeption(item.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl,
                    },
                    Price = product.Price,
                    Quantity = item.Quantity

                };
                orderItems.Add(orderItem);
            }

            // Get Delivery Method
            var deliveryMethod = await _unitOfWork.GetRepostiry<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);


            var order = new Order(email, orderAdrress, deliveryMethod, orderItems, subTotal);

             await _unitOfWork.GetRepostiry<Order, Guid>().AddAsync(order);

             await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }
        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepostiry<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string email)
        {
            var spec = new OrderSpecifications(email);
            var orders =  await _unitOfWork.GetRepostiry<Order,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }


        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecifications(id);
            var order = await _unitOfWork.GetRepostiry<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
