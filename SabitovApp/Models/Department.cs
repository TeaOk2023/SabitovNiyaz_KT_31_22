using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SabitovApp.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int? HeadOfDepartmentId { get; set; }

        [ForeignKey("HeadOfDepartmentId")]
        public Teacher HeadOfDepartment { get; set; }
    }
}
