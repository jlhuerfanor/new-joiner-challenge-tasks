using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Tasks.Model.Domain;

namespace  Tasks.Service.Persistence {

    public abstract class TaskDbContext : DbContext {
        public const string DefaultSchemaName = "task";

        protected TaskDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Task> Tasks { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DefaultSchemaName);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(TaskDbContext)));
        }
    }
}