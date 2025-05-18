using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SabitovApp.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public int? AcademicDegreeId { get; set; } 

        [ForeignKey("AcademicDegreeId")]
        public AcademicDegree AcademicDegree { get; set; }

        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        // Навигационное свойство для дисциплин, которые ведет преподаватель
        public ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
    }
}
