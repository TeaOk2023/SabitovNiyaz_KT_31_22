using Microsoft.EntityFrameworkCore;
using SabitovApp.Data;
using SabitovApp.Filters.DisciplineFilters;
using SabitovApp.Models;
using SabitovApp.Models.DTO;

namespace SabitovApp.Interfaces.WorkloadInterface
{
    public interface IDisciplineService
    {
        public Task<DisciplineDTO[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken);
    }

    public class DisciplineService : IDisciplineService
    {
        private readonly StudentDbContex _dbContex;

        public DisciplineService(StudentDbContex dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<DisciplineDTO[]> GetDisciplinesByFilterAsync(DisciplineFilter filter, CancellationToken cancellationToken)
        {
            var discipline = _dbContex.Set<Discipline>().Include(x => x.Workloads).Include(x => x.Teachers).AsQueryable();


            if (!string.IsNullOrEmpty(filter.TeacherName))
            {
                discipline = discipline.Where(w => w.Teachers.Any(t => t.FirstName == filter.TeacherName));
            }

            if (filter.start_hours.HasValue && filter.start_hours.Value != null)
            {
                discipline = discipline.Where(d => d.Workloads.Any(w => w.Hours >= filter.start_hours.Value));
            }

            if (filter.end_hours.HasValue)
            {
                discipline = discipline.Where(d => d.Workloads.Any(w => w.Hours <= filter.end_hours.Value));
            }

            if (filter.start_hours > filter.end_hours)
            {
                return null;
            }

            return await discipline.Select(d => new DisciplineDTO
            {
                DisciplineId = d.DisciplineId,
                Name = d.Name,
                TeacherNames = d.Teachers.Select(t => t.FirstName).ToList()
            }).ToArrayAsync(cancellationToken);
        }
    }
}
