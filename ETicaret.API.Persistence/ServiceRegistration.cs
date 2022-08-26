using ETicaret.API.Persistence.Concretes;
using ETicaret.API.Persistence.Contexts;
using ETicaretAPI.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ETicaretAPI.Application.Repositories;
using ETicaret.API.Persistence.Repositories;

namespace ETicaret.API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseSqlServer
            (configuration.GetConnectionString("SqlServerConnectionStrings")));

            //AddSingleTon kullanmamamızın nedeni;
            //Addscoped her request için  1 defalığına 1 referans(instance) oluşturup onu kullanıyor;
            //AddSingleTon ise tüm requestler için 1 tane oluşturup ve tüm uygulama boyunca onu kullanıyor
            //Burada sağlıklı olan AddSingleton'dır.
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        }
    }
}
