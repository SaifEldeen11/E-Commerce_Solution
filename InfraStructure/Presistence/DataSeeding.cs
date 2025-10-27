using Domain.Contracts;
using Domain.Models;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data.Contexts;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding : IDataSeeding
    {
        private readonly StoreDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeeding(StoreDbContext dbContext, RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> _userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = _userManager;
        }

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
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodsData = File.OpenRead("C:\\Users\\merom\\Desktop\\RouteBackEnd\\API\\E-Commerce_Solution\\InfraStructure\\Presistence\\Data\\DataSeeding\\delivery.json");
                    var deliveryMethods =  await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodsData);

                    if (deliveryMethods is not null && deliveryMethods.Any())
                    {
                       await _dbContext.Set<DeliveryMethod>().AddRangeAsync(deliveryMethods);
                    }
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // to do 

            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Aly",
                        UserName = "MohamedAly",
                        PhoneNumber = "0123456789"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "Salam@gmail.com",
                        DisplayName = "Salama Ahmed",
                        UserName = "SalamaAhmed",
                        PhoneNumber = "0123451370"
                    };

                    await _userManager.CreateAsync(user01, "P@ssw0rd");
                    await _userManager.CreateAsync(user02, "P@ssw0rd");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch (Exception ex)
            {
               // To do 
            }
        }


    }
}
