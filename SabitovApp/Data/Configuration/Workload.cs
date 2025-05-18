using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class WorkloadConfiguration : IEntityTypeConfiguration<Workload>
    {
        public void Configure(EntityTypeBuilder<Workload> builder)
        {
            builder.HasKey(w => w.WorkloadId);
            builder.Property(w => w.WorkloadId).ValueGeneratedOnAdd();
            builder.Property(w => w.WorkloadId)
                   .HasColumnName("WorkloadId")
                   .HasComment("Идентификатор нагрузки");

            builder.Property(w => w.Hours)
                   .HasColumnName("Hours")
                   .IsRequired()
                   .HasComment("Количество часов нагрузки");

            builder.Property(w => w.DisciplineId)
                   .HasColumnName("DisciplineId")
                   .IsRequired()
                   .HasComment("Идентификатор дисциплины (ссылка на DisciplineId)");

            builder.HasOne(w => w.Discipline)
                   .WithMany(d => d.Workloads)
                   .HasForeignKey(w => w.DisciplineId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
