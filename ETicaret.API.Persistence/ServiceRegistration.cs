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

namespace ETicaret.API.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ETicaretAPIDbContext>(options => options.UseSqlServer
            (configuration.GetConnectionString("SqlServerConnectionStrings")));
        }
    }
}
