using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class AcademicDegreeConfiguration : IEntityTypeConfiguration<AcademicDegree>
    {
        public void Configure(EntityTypeBuilder<AcademicDegree> builder)
        {      
            builder.HasKey(ad => ad.AcademicDegreeId);
            builder.Property(ad => ad.AcademicDegreeId).ValueGeneratedOnAdd();

            builder.Property(ad => ad.AcademicDegreeId)
                   .HasColumnName("AcademicDegreeId")
                   .HasComment("Идентификатор ученой степени");

            builder.Property(ad => ad.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasComment("Название ученой степени");

            builder.HasMany(ad => ad.Teachers)
                   .WithOne(t => t.AcademicDegree)
                   .HasForeignKey(t => t.AcademicDegreeId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
