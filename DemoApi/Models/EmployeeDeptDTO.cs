using System.ComponentModel.DataAnnotations;

namespace DemoApi.Models
{
    public class EmployeeDeptDTO
    {
        [Key]
        public int EID { get; set; }
        [Required]
        public string? EName { get; set; }
        [Required]
        [Range(20000, 1000000)]
        public double ESalary { get; set; }

        public string DepartmentName { get; set; }
        public int DeptId   { get; set; }
    }
}
