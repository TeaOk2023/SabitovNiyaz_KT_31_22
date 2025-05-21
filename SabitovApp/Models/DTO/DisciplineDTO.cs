using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models.DTO
{
    public class DisciplineDTO
    {
        public int DisciplineId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<string>? TeacherNames { get; set; }
    }
}
