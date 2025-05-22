using Microsoft.EntityFrameworkCore;
using SabitovApp.Data;
using SabitovApp.Models;
using SabitovApp.Models.DTO;

namespace SabitovApp.Interfaces.WorkloadInterface
{
    public interface IWorkloadService
    {
        public Task<WorkloadDTO[]> GetWorkloadsAsync(CancellationToken cancellationToken);

        public Task<WorkloadDTO?> GetWorkloadByIdAsync(int id, CancellationToken cancellationToken);
        Task<WorkloadCreateDTO> AddWorkloadAsync(WorkloadCreateDTO workloadDto, CancellationToken cancellationToken);
        Task UpdateWorkloadAsync(int id, WorkloadDTO workload, CancellationToken cancellationToken);
        Task DeleteWorkloadAsync(int id, CancellationToken cancellationToken);
    }

    public class WorkloadService : IWorkloadService
    {
        private readonly StudentDbContex _dbContex;

        public WorkloadService(StudentDbContex dbContex)
        {
            _dbContex = dbContex;
        }

        public async Task<WorkloadDTO[]> GetWorkloadsAsync(CancellationToken cancellationToken)
        {
            var workloads = _dbContex.Set<Workload>().Include(x => x.Discipline).Where(x => x.isDeleted == false).AsQueryable();


            return await workloads.Select(w => new WorkloadDTO
            {
                WorkloadId = w.WorkloadId,
                Hours = w.Hours,
                //DisciplineId = w.DisciplineId,
                DisciplineName = w.Discipline.Name, 
                isDeleted = w.isDeleted
            }).ToArrayAsync(cancellationToken);
        }

        public async Task<WorkloadDTO?> GetWorkloadByIdAsync(int id, CancellationToken cancellationToken)
        {
            var workload = await _dbContex.Set<Workload>()
                .Include(w => w.Discipline)
                .FirstOrDefaultAsync(w => w.WorkloadId == id, cancellationToken);

            if (workload == null)
            {
                return null;
            }

            return new WorkloadDTO
            {
                WorkloadId = workload.WorkloadId,
                Hours = workload.Hours,
                //DisciplineId = workload.DisciplineId,
                DisciplineName = workload.Discipline.Name,
                isDeleted = workload.isDeleted
            };
        }

        public async Task<WorkloadCreateDTO> AddWorkloadAsync(WorkloadCreateDTO workloadDto, CancellationToken cancellationToken)
        {
            var workload = new Workload
            {
                Hours = workloadDto.Hours,
                DisciplineId = workloadDto.DisciplineId,
                isDeleted = false 
            };

            _dbContex.Set<Workload>().Add(workload);
            await _dbContex.SaveChangesAsync(cancellationToken);
            //workloadDto.WorkloadId = workload.WorkloadId;

            return workloadDto;
        }


        public async Task UpdateWorkloadAsync(int id, WorkloadDTO workloadDto, CancellationToken cancellationToken)
        {

            var workload = await _dbContex.Set<Workload>().FindAsync(id);

            if (workload == null)
            {
                return;
            }

            workload.Hours = workloadDto.Hours;
            workload.DisciplineId = workloadDto.DisciplineId;
            //workload.isDeleted = workloadDto.isDeleted;

            await _dbContex.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteWorkloadAsync(int id, CancellationToken cancellationToken) 
        {
            var workload = await _dbContex.Set<Workload>().FindAsync(id);

            if (workload == null)
            {
                return;
            }

            workload.isDeleted = true; 
            await _dbContex.SaveChangesAsync(cancellationToken);
        }
    }
}
