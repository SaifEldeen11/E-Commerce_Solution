// This Code Was Made By Eng Saif :)
using AutoMapper;
using Domain.Contracts;
using E_Commerce.Web.CustomMiddleWare;
using E_Commerce.Web.Extentions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presistence;
using Presistence.Data.Contexts;
using Presistence.Repostiries;
using ServiceAbstraction;
using ServiceImplemntation;
using ServiceImplemntation.MappingProfiles;
using Shared.ErrorModels;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            #region Services
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerServices();



            builder.Services.AddInfraStructureServices(builder.Configuration);

            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();

            builder.Services.AddJWTServices(builder.Configuration);
            #endregion

            var app = builder.Build();


            #region Data Seeding
            await app.SeedDataAsync();

            #endregion



            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
