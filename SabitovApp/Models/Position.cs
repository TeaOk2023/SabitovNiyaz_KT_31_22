using System.ComponentModel.DataAnnotations;

namespace SabitovApp.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } 

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
