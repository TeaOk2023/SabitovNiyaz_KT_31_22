using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SabitovApp.Data;
using SabitovApp.Filters.DisciplineFilters;
using SabitovApp.Interfaces.WorkloadInterface;
using SabitovApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabitovNiyazKT_31_22.Tests
{
    public class DisciplineServiceTests
    {
        private DbContextOptions<StudentDbContex> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<StudentDbContex>();
            builder.UseInMemoryDatabase("TestDB")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        [Fact]
        public async Task GetDisciplinesByFilterAsync_TeacherNameFilter_ReturnsCorrectDisciplines()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                // Create the schema in the database
                context.Database.EnsureCreated();

                // Seed some data into the database
                context.Set<Discipline>().AddRange(
                     new Discipline { DisciplineId = 1, Name = "Math", Teachers = new List<Teacher> { new Teacher { FirstName = "Alice", LastName = "Smith" } } },
                     new Discipline { DisciplineId = 2, Name = "Physics", Teachers = new List<Teacher> { new Teacher { FirstName = "Bob", LastName = "Johnson" } } },
                     new Discipline { DisciplineId = 3, Name = "Chemistry", Teachers = new List<Teacher> { new Teacher { FirstName = "Alice", LastName = "Smith" } } }
                );
                context.SaveChanges();
            }


            using (var context = new StudentDbContex(options))
            {

                var disciplineService = new DisciplineService(context);
                var filter = new DisciplineFilter { TeacherName = "Alice" };

                // Act
                var result = await disciplineService.GetDisciplinesByFilterAsync(filter, CancellationToken.None);

                // Assert
                Assert.Equal(2, result.Length);
                Assert.Contains(result.Select(d => d.Name), name => name == "Math");
                Assert.Contains(result.Select(d => d.Name), name => name == "Chemistry");

            }
        }

        [Fact]
        public async Task GetDisciplinesByFilterAsync_HoursRangeFilter_ReturnsCorrectDisciplines()
        {
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                context.Database.EnsureCreated();

                context.Set<Discipline>().AddRange(
                     new Discipline { DisciplineId = 1, Name = "Math", Workloads = new List<Workload> { new Workload { Hours = 20 } } },
                     new Discipline { DisciplineId = 2, Name = "Physics", Workloads = new List<Workload> { new Workload { Hours = 35 } } },
                     new Discipline { DisciplineId = 3, Name = "Chemistry", Workloads = new List<Workload> { new Workload { Hours = 40 } } }
                );
                context.SaveChanges();
            }


            using (var context = new StudentDbContex(options))
            {

                var disciplineService = new DisciplineService(context);
                var filter = new DisciplineFilter { start_hours = 30, end_hours = 45 };

                var result = await disciplineService.GetDisciplinesByFilterAsync(filter, CancellationToken.None);

                Assert.Equal(2, result.Length);
                Assert.Contains(result.Select(d => d.Name), name => name == "Physics");
                Assert.Contains(result.Select(d => d.Name), name => name == "Chemistry");

            }
        }
    }
}
