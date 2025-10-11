// This Code Was Made By Eng Saif :)
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data.Contexts;
using Presistence.Repostiries;
using ServiceAbstraction;
using ServiceImplemntation;
using ServiceImplemntation.MappingProfiles;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection; // Add this at the top with other using directives

namespace E_Commerce.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(ServiceImplemntation.AssemblyRefrence));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceManger, ServiceManger>();
            #endregion

            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var seed = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            seed.DataSeedAsync();



            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
