using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SabitovApp.Models
{
    public class Workload
    {
        [Key]
        public int WorkloadId { get; set; }

        [Required]
        public int Hours { get; set; } 

        public int DisciplineId { get; set; }

        [ForeignKey("DisciplineId")]
        public Discipline Discipline { get; set; }
    }
}
