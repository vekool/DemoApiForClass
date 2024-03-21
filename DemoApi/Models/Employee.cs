using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EID { get; set; }
        [Required]
        public string? EName { get; set; }
        [Required]
        [Range(20000, 1000000)]
        public double ESalary { get; set; }

        public int? DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department Department { get; }
    }
}
