using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models.DTO
{
    public class TeacherDTO
    {
        public int TeacherId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public int? AcademicDegreeId { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
    }

}
