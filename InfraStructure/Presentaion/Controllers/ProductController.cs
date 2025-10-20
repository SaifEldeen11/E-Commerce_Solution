using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared;
using Shared.Dtos.ProductModule;

namespace Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // baseUrl/api/Product
    public class ProductController(IServiceManger _serviceManger):ControllerBase
    {
        // Get All Products
        // NameAsc
        // NameDesc
        // PriceAsc
        // PriceDesc
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryPrams queryPrams)
        {
            var products =await _serviceManger.ProductService.GettAllProductsAsync(queryPrams);
            return Ok(products);
        }


        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var product =   await _serviceManger.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }

        // Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands =   await _serviceManger.ProductService.GetAllBrandsAsync();
            return Ok(brands);
        }

        // Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types =   await _serviceManger.ProductService.GetAllTypesAsync();
            return Ok(types);
        }
    }
}
