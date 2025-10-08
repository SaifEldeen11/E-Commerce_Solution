using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                // Empty Database then do seeding
                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.ReadAllText("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);

                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                    }
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypesData = File.ReadAllText("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);

                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                    }
                }

                if (!_dbContext.Products.Any())
                {
                    var ProductsData = File.ReadAllText("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (Products is not null && Products.Any())
                    {
                        _dbContext.Products.AddRange(Products);
                    }
                }

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during data seeding: {ex.Message}");
            }
        }
    }
}
