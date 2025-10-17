using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using ServiceImplemntation.MappingProfiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplemntation
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(con => con.AddProfile(new ProductProfile()), typeof(ServiceImplemntation.AssemblyRefrence));
            Services.AddScoped<IServiceManger, ServiceManger>();

            return Services;
        }
    }
}
