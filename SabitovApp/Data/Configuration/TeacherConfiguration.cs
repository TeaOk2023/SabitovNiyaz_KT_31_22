using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.TeacherId);
            builder.Property(t => t.TeacherId).ValueGeneratedOnAdd();

            builder.Property(t => t.TeacherId)
                   .HasColumnName("TeacherId")
                   .HasComment("Идентификатор преподавателя");

            builder.Property(t => t.FirstName)
                   .HasColumnName("FirstName")
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasComment("Имя преподавателя");

            builder.Property(t => t.LastName)
                   .HasColumnName("LastName")
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasComment("Фамилия преподавателя");

            builder.Property(t => t.AcademicDegreeId)
                   .HasColumnName("AcademicDegreeId")
                   .HasComment("Ученая степень");

            builder.Property(t => t.PositionId)
                   .HasColumnName("PositionId")
                   .IsRequired()
                   .HasComment("Должность");

            builder.Property(t => t.DepartmentId)
                   .HasColumnName("DepartmentId")
                   .IsRequired()
                   .HasComment("Идентификатор кафедры");

            builder.HasOne(t => t.AcademicDegree)
                   .WithMany(ad => ad.Teachers)
                   .HasForeignKey(t => t.AcademicDegreeId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Position)
                   .WithMany(p => p.Teachers)
                   .HasForeignKey(t => t.PositionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(t => t.Disciplines)
                   .WithMany(d => d.Teachers)
                   .UsingEntity(j => j.ToTable("TeacherDisciplines").HasComment("Связь между преподавателями и дисциплинами"));
        }
    }
}
