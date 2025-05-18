using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {

            builder.HasKey(d => d.DepartmentId);
            builder.Property(d => d.DepartmentId).ValueGeneratedOnAdd();

            builder.Property(d => d.DepartmentId)
                   .HasColumnName("DepartmentId")
                   .HasComment("Идентификатор кафедры");

            builder.Property(d => d.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(100)
                   .IsRequired()
                   .HasComment("Название кафедры");

            builder.Property(d => d.HeadOfDepartmentId)
                   .HasColumnName("HeadOfDepartmentId")
                   .HasComment("Идентификатор заведующего кафедрой (ссылка на TeacherId)");

            
            builder.HasOne(d => d.HeadOfDepartment)
                   .WithOne() 
                   .HasForeignKey<Department>(d => d.HeadOfDepartmentId)
                   .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
