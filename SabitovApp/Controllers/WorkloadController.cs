using Microsoft.AspNetCore.Mvc;
using SabitovApp.Interfaces.WorkloadInterface;
using SabitovApp.Models;
using SabitovApp.Models.DTO;

namespace SabitovApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkloadController : ControllerBase
    {
        private readonly ILogger<WorkloadController> _logger;
        private readonly IWorkloadService _workloadService;

        public WorkloadController(ILogger<WorkloadController> logger, IWorkloadService workloadService)
        {
            _logger = logger;
            _workloadService = workloadService;
        }


        [HttpGet(Name = "GetWorkloads")]
        public async Task<IActionResult> GetWorkloadsAsync(CancellationToken cancellationToken = default)
        {
            var workloads = await _workloadService.GetWorkloadsAsync(cancellationToken);

            if (workloads == null)
            {
                return NotFound();
            }

            return Ok(workloads);
        }

        [HttpGet("{id}", Name = "GetWorkloadById")]
        public async Task<IActionResult> GetWorkloadByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"GetWorkloadByIdAsync endpoint called with id: {id}");
            var workload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);

            if (workload == null)
            {
                _logger.LogWarning($"Workload with id: {id} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Successfully retrieved workload with id: {id}.");
            return Ok(workload);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkloadAsync([FromBody] WorkloadCreateDTO workloadDto, CancellationToken cancellationToken = default)
        {
            var createdWorkload = await _workloadService.AddWorkloadAsync(workloadDto, cancellationToken);

            if (createdWorkload == null)
            {
                return BadRequest("Ошибка создания");
            }

            return CreatedAtAction("GetWorkloadById", createdWorkload);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkloadAsync(int id, [FromBody] WorkloadDTO workloadDto, CancellationToken cancellationToken = default)
        {

            if (id != workloadDto.WorkloadId)
            {
                return BadRequest("ID не совпали");
            }

            var existingWorkload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);

            if (existingWorkload == null)
            {
                return NotFound();
            }

            await _workloadService.UpdateWorkloadAsync(id, workloadDto, cancellationToken);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkloadAsync(int id, CancellationToken cancellationToken = default)
        {
            var existingWorkload = await _workloadService.GetWorkloadByIdAsync(id, cancellationToken);

            if (existingWorkload == null)
            {
                return NotFound();
            }

            await _workloadService.DeleteWorkloadAsync(id, cancellationToken);
            return NoContent();
        }
    }
}
