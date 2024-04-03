using Managment.core.Database.Interfaces;
using Managment.core.Database.Models;
using Managment.core.Database.Services;
using Managment.core.Repositories.Facturas.Interfaces;
using Managment.core.Repositories.Facturas.Services;
using Managment.core.Repositories.Personas.Interfaces;
using Managment.core.Repositories.Personas.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Managment.core
{
    public static class ManagmentCore
    {
        public static void implementServices(this IServiceCollection services, DatabaseConfigurator dbconfigurator )
        {
            if (String.IsNullOrWhiteSpace(dbconfigurator.ConnectionString)) throw new Exception("Please provide a db connection string");

            services.AddScoped<IDatabaseInitializer>(services =>
            {
                return new SqliteDatabaseInitializer(dbconfigurator.ConnectionString);
            });

            services.AddScoped<IPersonasRepository, DirectorioService>();
            services.AddScoped<IFacturasRepository, VentasService>();
        }
    }
}