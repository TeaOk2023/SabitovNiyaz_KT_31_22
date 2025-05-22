using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models.DTO
{
    public class WorkloadCreateDTO
    {
        /// <summary>
        /// public int WorkloadId { get; set; }
        /// </summary>

        [Required]
        public int Hours { get; set; }

        public int DisciplineId { get; set; }

        //public bool isDeleted { get; set; }

        //public string? DisciplineName { get; set; }
    }
}
