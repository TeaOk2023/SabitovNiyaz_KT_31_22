using Microsoft.EntityFrameworkCore;
using SabitovApp.Controllers;
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
    public class DisciplineInTests
    {
        private readonly DbContextOptions<StudentDbContex> _dbContextOptions;

        public DisciplineInTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudentDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new StudentDbContex(_dbContextOptions))
            {

                context.Database.EnsureCreated();

                if (!context.Set<Discipline>().Any())
                {
                    context.Set<Discipline>().AddRange(
                        new Discipline { DisciplineId = 1, Name = "Math", Teachers = new List<Teacher> { new Teacher { FirstName = "Alice", LastName = "Smith" } }, Workloads = new List<Workload> { new Workload { Hours = 30 } } },
                        new Discipline { DisciplineId = 2, Name = "Physics", Teachers = new List<Teacher> { new Teacher { FirstName = "Bob", LastName = "Johnson" } }, Workloads = new List<Workload> { new Workload { Hours = 40 } } },
                        new Discipline { DisciplineId = 3, Name = "Chemistry", Teachers = new List<Teacher> { new Teacher { FirstName = "Alice", LastName = "Smith" } }, Workloads = new List<Workload> { new Workload { Hours = 50 } } }
                    );
                    context.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task GetDisciplinesByFilterAsync_TeacherName_ReturnsCorrectDisciplines()
        {
            // Arrange
            using (var context = new StudentDbContex(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(context);
                var filter = new DisciplineFilter { TeacherName = "Alice" };


                var result = await disciplineService.GetDisciplinesByFilterAsync(filter, CancellationToken.None);

                Assert.Equal(2, result.Length);
                Assert.Contains(result.Select(d => d.Name), name => name == "Math");
                Assert.Contains(result.Select(d => d.Name), name => name == "Chemistry");
            }
        }

        [Fact]
        public async Task GetDisciplinesByFilterAsync_HoursRange_ReturnsCorrectDisciplines()
        {
            // Arrange
            using (var context = new StudentDbContex(_dbContextOptions))
            {
                var disciplineService = new DisciplineService(context);
                var filter = new DisciplineFilter { start_hours = 35, end_hours = 55 };

                // Act
                var result = await disciplineService.GetDisciplinesByFilterAsync(filter, CancellationToken.None);

                // Assert
                Assert.Equal(2, result.Length);
                Assert.Contains(result.Select(d => d.Name), name => name == "Physics");
                Assert.Contains(result.Select(d => d.Name), name => name == "Chemistry");
            }
        }
    }
}

