using AutoMapper;
using Domain.Contracts;
using Domain.Exeptions;
using Domain.Models;
using Domain.Models.ProductModule;
using ServiceAbstraction;
using ServiceImplemntation.Specifications;
using Shared;
using Shared.Dtos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var repo = _unitOfWork.GetRepostiry<ProductBrand, int>();
            var brands = await repo.GetAllAsync();

            var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);

            return brandsDto;
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var types= await _unitOfWork.GetRepostiry<ProductType,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);

        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepostiry<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecificaations(id);
            var product = await repo.GetByIdAsync(specification);
            if (product == null) throw new ProductNotFoundExeption(id);
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<PaginatedResult<ProductDto>> GettAllProductsAsync(ProductQueryPrams queryPrams)
        {
            var repo = _unitOfWork.GetRepostiry<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecificaations(queryPrams);
            var products = await repo.GetAllAsync(specification);

            var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);

            var productCount= productsDto.Count();
            var CountSpec= new ProductCountSpecfications(queryPrams);
            var totalCount = await repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(productCount,queryPrams.PageIndex ,0, productsDto)
            {
                 
            };

        }
    }
}
