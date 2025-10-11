using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ((await _dbContext.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // Empty Database then do seeding
                if (!_dbContext.ProductBrands.Any())
                {
                    //var ProductBrandsData =  await File.ReadAllTextAsync("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\brands.json");
                    var ProductBrandsData = File.OpenRead("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\brands.json");
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);

                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                         await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.OpenRead("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\types.json");
                    var ProductTypes =  await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypesData);

                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                         await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.OpenRead("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\products.json");
                    var Products =  await JsonSerializer.DeserializeAsync<List<Product>>(ProductsData);

                    if (Products is not null && Products.Any())
                    {
                       await _dbContext.Products.AddRangeAsync(Products);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // to do 

            }
        }
    }
}
