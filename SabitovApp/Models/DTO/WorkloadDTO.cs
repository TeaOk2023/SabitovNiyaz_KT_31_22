using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models.DTO
{
    public class WorkloadDTO
    {
        public int WorkloadId { get; set; }

        [Required]
        public int Hours { get; set; }

        public int DisciplineId { get; set; }

        public bool isDeleted { get; set; }

        public string? DisciplineName { get; set; }
    }
}
