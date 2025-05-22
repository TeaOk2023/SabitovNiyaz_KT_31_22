using Microsoft.EntityFrameworkCore;
using SabitovApp.Data;
using SabitovApp.Interfaces.WorkloadInterface;
using SabitovApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabitovNiyazKT_31_22.Tests
{
    public class WorkloadInTests
    {
        [Fact]
        public async Task GetWorkloadsAsync_TwoWorkloadsExist_ReturnsTwoWorkloadDTOs()
        {
            var options = new DbContextOptionsBuilder<StudentDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDb_GetWorkloadsAsync_TwoWorkloads")
                .Options;

            using (var context = new StudentDbContex(options))
            {
                var discipline1 = new Discipline { DisciplineId = 1, Name = "Math" };
                var discipline2 = new Discipline { DisciplineId = 2, Name = "Physics" };
                context.Set<Discipline>().AddRange(discipline1, discipline2);
                await context.SaveChangesAsync();

                var workload1 = new Workload { Hours = 20, DisciplineId = 1, isDeleted = false };
                var workload2 = new Workload { Hours = 30, DisciplineId = 2, isDeleted = false };
                context.Set<Workload>().AddRange(workload1, workload2);
                await context.SaveChangesAsync();

                var service = new WorkloadService(context);


                // Act
                var result = await service.GetWorkloadsAsync(CancellationToken.None);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Length);
                Assert.Contains(result, w => w.Hours == 20);
                Assert.Contains(result, w => w.Hours == 30);
                Assert.Contains(result, w => w.DisciplineName == "Math");
                Assert.Contains(result, w => w.DisciplineName == "Physics");
            }
        }

        [Fact]
        public async Task DeleteWorkloadAsync_WorkloadExists_WorkloadIsDeleted()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<StudentDbContex>()
                .UseInMemoryDatabase(databaseName: "TestDb_DeleteWorkloadAsync_WorkloadExists")
                .Options;

            using (var context = new StudentDbContex(options))
            {
                // Добавляем фейковые данные
                var discipline1 = new Discipline { DisciplineId = 1, Name = "Math" };
                context.Set<Discipline>().Add(discipline1);
                await context.SaveChangesAsync();

                var workload = new Workload { WorkloadId = 1, Hours = 20, DisciplineId = 1, isDeleted = false };
                context.Set<Workload>().Add(workload);
                await context.SaveChangesAsync();
                var service = new WorkloadService(context);

                // Act
                await service.DeleteWorkloadAsync(1, CancellationToken.None); // Удаляем Workload с ID 1

                // Assert
                var deletedWorkload = await context.Set<Workload>().FindAsync(1);
                Assert.NotNull(deletedWorkload);
                Assert.True(deletedWorkload.isDeleted);
            }
        }
    }
}
