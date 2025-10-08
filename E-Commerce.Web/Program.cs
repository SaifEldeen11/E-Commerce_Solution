using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data.Contexts;
using System.Security.Cryptography;

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
            #endregion

            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var seed = scope.ServiceProvider.GetRequiredService<IDataSeeding>();

            seed.DataSeed();



            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();



            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
