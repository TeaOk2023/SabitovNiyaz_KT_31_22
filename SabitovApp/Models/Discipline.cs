using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models
{
    public class Discipline
    {
        [Key]
        public int DisciplineId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        // конект с нагрузкой
        public ICollection<Workload> Workloads { get; set; } = new List<Workload>();
    }
}
