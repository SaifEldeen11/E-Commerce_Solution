using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.Specifications
{
    internal class ProductCountSpecfications:BaseSpecficstion<Product, int>
    {
        public ProductCountSpecfications(ProductQueryPrams queryPrams): base(p => (!queryPrams.brandId.HasValue|| p.BrandId == queryPrams.brandId) 
              && (!queryPrams.TypeId.HasValue || p.TypeId == queryPrams.TypeId)
        && (string.IsNullOrWhiteSpace(queryPrams.SearchValue) || p.Name.ToLower().Contains(queryPrams.SearchValue.ToLower()))) 
        {
            
        }
    }
}
