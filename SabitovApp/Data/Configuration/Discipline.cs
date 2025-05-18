using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(d => d.DisciplineId);
            builder.Property(d => d.DisciplineId).ValueGeneratedOnAdd();
            builder.Property(d => d.DisciplineId)
                   .HasColumnName("DisciplineId")
                   .HasComment("Идентификатор дисциплины");

            builder.Property(d => d.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasComment("Название дисциплины");

            builder.HasMany(d => d.Workloads)
                   .WithOne(w => w.Discipline)
                   .HasForeignKey(w => w.DisciplineId)
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(d => d.Teachers)
                   .WithMany(t => t.Disciplines)
                   .UsingEntity(j => j.ToTable("TeacherDisciplines").HasComment("Связь между преподавателями и дисциплинами")); // Используем существующую таблицу связей из TeacherConfiguration
        }
    }
}
