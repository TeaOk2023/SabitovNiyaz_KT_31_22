using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SabitovApp.Data;
using SabitovApp.Interfaces.WorkloadInterface;
using SabitovApp.Models;
using SabitovApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabitovNiyazKT_31_22.Tests
{
    public class WorkloadTests
    {
        private DbContextOptions<StudentDbContex> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<StudentDbContex>();
            builder.UseInMemoryDatabase("InMemoryDbForTesting")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        [Fact]
        public async Task GetWorkloadsAsync_ReturnsWorkloadDTOs()
        {
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                context.Database.EnsureCreated();
                context.Set<Workload>().AddRange(
                     new Workload { WorkloadId = 1, Hours = 20, Discipline = new Discipline { DisciplineId = 1, Name = "Math" }, isDeleted = false },
                     new Workload { WorkloadId = 2, Hours = 30, Discipline = new Discipline { DisciplineId = 2, Name = "Physics" }, isDeleted = false }
                );
                context.SaveChanges();
            }

            using (var context = new StudentDbContex(options))
            {
                var workloadService = new WorkloadService(context);

                var result = await workloadService.GetWorkloadsAsync(CancellationToken.None);

                Assert.Equal(2, result.Length);
                Assert.Contains(result.Select(x => x.Hours), w => w == 20);
                Assert.Contains(result.Select(x => x.Hours), w => w == 30);
                Assert.Contains(result.Select(x => x.DisciplineName), w => w == "Math");
                Assert.Contains(result.Select(x => x.DisciplineName), w => w == "Physics");
            }
        }

        [Fact]
        public async Task GetWorkloadByIdAsync_ExistingId_ReturnsWorkloadDTO()
        {
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                context.Database.EnsureCreated();
                context.Set<Workload>().Add(
                    new Workload { WorkloadId = 1, Hours = 20, Discipline = new Discipline { DisciplineId = 1, Name = "Math" }, isDeleted = false }
                );
                context.SaveChanges();
            }

            using (var context = new StudentDbContex(options))
            {
                var workloadService = new WorkloadService(context);

                var result = await workloadService.GetWorkloadByIdAsync(1, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(20, result.Hours);
                Assert.Equal("Math", result.DisciplineName);
            }
        }


        [Fact]
        public async Task AddWorkloadAsync_DisciplineExists_ReturnsWorkloadDTO()
        {
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                context.Database.EnsureCreated();
                context.Set<Discipline>().Add(
                    new Discipline { DisciplineId = 1, Name = "Math" }
                );
                context.SaveChanges();
            }

            using (var context = new StudentDbContex(options))
            {
                var workloadService = new WorkloadService(context);
                var workloadCreateDto = new WorkloadCreateDTO { Hours = 20, DisciplineId = 1 };

                var result = await workloadService.AddWorkloadAsync(workloadCreateDto, CancellationToken.None);

                Assert.NotNull(result);
                Assert.Equal(20, result.Hours);
            }
        }

        [Fact]
        public async Task AddWorkloadAsync_DisciplineDoesNotExist_ReturnsNull()
        {
            var options = CreateNewContextOptions();

            using (var context = new StudentDbContex(options))
            {
                context.Database.EnsureCreated();

            }

            using (var context = new StudentDbContex(options))
            {
                var workloadService = new WorkloadService(context);
                var workloadCreateDto = new WorkloadCreateDTO { Hours = 20, DisciplineId = 1 };

                var result = await workloadService.AddWorkloadAsync(workloadCreateDto, CancellationToken.None);

                Assert.Null(result);
            }
        }
    }
}
