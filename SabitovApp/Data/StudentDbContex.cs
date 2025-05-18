using Microsoft.EntityFrameworkCore;
using SabitovApp.Data.Configuration;
using SabitovApp.Models;

namespace SabitovApp.Data
{
    public class StudentDbContex: DbContext
    {
        DbSet<Department> Departments { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<AcademicDegree> AcademicDegrees { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Discipline> Disciplines { get; set; }
        DbSet<Workload> Workloads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new AcademicDegreeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new DisciplineConfiguration());
            modelBuilder.ApplyConfiguration(new WorkloadConfiguration());
        }


        public StudentDbContex(DbContextOptions<StudentDbContex> options) : base(options)
        { 
            
        }        
    }
}
