using Microsoft.AspNetCore.Mvc;
using SabitovApp.Filters.DisciplineFilters;
using SabitovApp.Interfaces.WorkloadInterface;

namespace SabitovApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplineController : ControllerBase
    {
        private readonly ILogger<DisciplineController> _logger;
        private readonly IDisciplineService _disciplineService;
        private DisciplineService service;

        public DisciplineController(DisciplineService service)
        {
            this.service = service;
        }

        public DisciplineController(ILogger<DisciplineController> logger, IDisciplineService disciplineService)
        {
            _logger = logger;
            _disciplineService = disciplineService;
        }

    
        [HttpPost(Name = "GetDisciplinesByFilter")]
        public async Task<IActionResult> GetDisciplinesByFilterAsync([FromQuery] DisciplineFilter filter, CancellationToken cancellationToken = default)
        {
            var disciplines = await _disciplineService.GetDisciplinesByFilterAsync(filter, cancellationToken);

            if (disciplines == null || disciplines.Length == 0)
            {
                return NotFound();
            }

            return Ok(disciplines);
        }
    }
}
