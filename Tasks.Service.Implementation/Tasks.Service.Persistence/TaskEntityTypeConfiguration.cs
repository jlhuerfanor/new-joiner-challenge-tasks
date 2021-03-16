using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tasks.Model.Domain;
using Tasks.Service.Persistence.Conversion;

namespace Tasks.Service.Persistence
{
    public class TaskEntityTypeConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.ToTable("task");
            builder.Property(task => task.Id)
                .HasColumnName("id")
                .HasColumnType("int4")
                .ValueGeneratedOnAdd();
            builder.Property(tast => tast.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasColumnType("varchar(150)");
            builder.Property(tast => tast.Description)
                .HasColumnName("description")
                .HasColumnType("text");
            builder.Property(tast => tast.Completed)
                .HasColumnName("completed")
                .IsRequired()
                .HasColumnType("bool");
            builder.Property(tast => tast.EstimatedRequiredHours)
                .HasColumnName("estimated_required_hours")
                .IsRequired()
                .HasColumnType("int4");
            builder.Property(tast => tast.Stack)
                .HasColumnName("stack")
                .IsRequired()
                .HasColumnType("varchar(150)");
            builder.Property(task => task.MinimumRoles)
                .HasColumnType("text")
                .HasColumnName("minimum_roles")
                .HasConversion(new RoleListToJsonArrayConverter());
            builder.Property(task => task.AssignedIdNumber)
                .HasColumnName("assigned_id_number")
                .IsRequired()
                .HasColumnType("bigint");
            builder.Property(task => task.AssignedIdNumber)
                .HasColumnName("assigned_id_number")
                .IsRequired()
                .HasColumnType("bigint");
            builder.Property<int?>("ParentTaskId")
                .HasColumnName("parent_task_id")
                .HasColumnType("int4")
                .IsRequired(false);

            builder.HasMany(task => task.LinkedTasks)
                .WithOne(task => task.ParentTask)
                .HasForeignKey("ParentTaskId");
            builder.HasKey(task => task.Id)
                .HasName("pk_task_task_id");
        }
    }

}