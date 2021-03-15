using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace  Tasks.Service.Persistence {

    public abstract class TaskDbContext : DbContext {
        public const string DefaultSchemaName = "task";

        protected TaskDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TaskDbContext)));
        }
    }
}