﻿using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Extentions
{
    public static  class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();

            return Services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenrateApiValidationErrorsResponse;


            });
            return Services;
        }
    }
}
