using Domain.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.Specifications
{
    internal class OrderSpecifications:BaseSpecficstion<Order,Guid>
    {
        // Get all orders by Email 
        public OrderSpecifications(string email):base(o => o.UserEmail== email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }
        public OrderSpecifications(Guid id):base(o => o.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
