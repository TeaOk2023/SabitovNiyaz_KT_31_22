using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SabitovApp.Models;

namespace SabitovApp.Data.Configuration
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.PositionId);
            builder.Property(p => p.PositionId).ValueGeneratedOnAdd();

            builder.Property(p => p.PositionId)
                   .HasColumnName("PositionId")
                   .HasComment("Идентификатор должности");

            builder.Property(p => p.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired()
                   .HasComment("Название должности");

            builder.HasMany(p => p.Teachers)
                   .WithOne(t => t.Position)
                   .HasForeignKey(t => t.PositionId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}
