using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Tasks.Service.Persistence;

namespace Microsoft.Extensions.DependencyInjection {

    public class DefaultTaskDbContext : TaskDbContext
    {
        public DefaultTaskDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    public static class PersistenceConfiguration {
        public const string ConnectionStringName = "TaskConnectionString";

        public static IServiceCollection ConfigurePersistence(this IServiceCollection services,  IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString(ConnectionStringName);

            services.AddDbContext<DbContext, DefaultTaskDbContext>(options => options.UseNpgsql(connectionString));
            Console.WriteLine("Set up persistence provider.");

            return services;
        }
    }

}