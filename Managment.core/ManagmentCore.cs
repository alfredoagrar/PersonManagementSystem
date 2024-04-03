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

            string ConnString = dbconfigurator.ConnectionString;
            services.AddTransient<IDatabaseInitializer>(services =>
            {
                return new SqliteDatabaseInitializer(ConnString);
            });


            services.AddScoped<IPersonasRepository>(services =>
            {
                return new DirectorioService(ConnString);
            });

            services.AddScoped<IFacturasRepository>(services =>
            {
                return new VentasService(ConnString);
            });
        }
    }
}