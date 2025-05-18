using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models
{
    public class AcademicDegree
    {
        [Key]
        public int AcademicDegreeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } 

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
