using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BasketModule
{
    public class Basket
    {
        public string Id { get; set; } // Guid : created from the client side

        public ICollection<BasketItem> Items { get; set; } = [];



    }
}
