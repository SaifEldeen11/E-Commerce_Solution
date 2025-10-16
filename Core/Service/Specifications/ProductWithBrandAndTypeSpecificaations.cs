using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation.Specifications
{
    internal class ProductWithBrandAndTypeSpecificaations : BaseSpecficstion<Product, int>
    {
        // get all products 

        // case 1 : BrandId and TypeId are null
        // case 2 : BrandId null and TypeId value p => p.typeId == p.TypeId
        // case 3 : BrandId value and TypeId null p => p.brandId == p.BrandId
        // case 4 : BrandId value and TypeId value p => p.brandId == p.BrandId && p.typeId == p.TypeId


        public ProductWithBrandAndTypeSpecificaations(ProductQueryPrams queryPrams)
        : base(p => (!queryPrams.brandId.HasValue|| p.BrandId == queryPrams.brandId) 
              && (!queryPrams.TypeId.HasValue || p.TypeId == queryPrams.TypeId)
        && (string.IsNullOrWhiteSpace(queryPrams.SearchValue) || p.Name.ToLower().Contains(queryPrams.SearchValue.ToLower()))) 
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);

            switch (queryPrams.SortingOption)
            {
                case ProductSortingOption.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOption.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOption.PriceDesc:
                AddOrderByDescending(p => p.Price);
                break;

                   default:
                    break;
            }

            ApplyPaging(queryPrams.PageSize,queryPrams.PageIndex);
        }

        // get product by id
        public ProductWithBrandAndTypeSpecificaations(int id):base(p=>p.Id==id)
        {
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}
