using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Models
{
    /// <summary>
    /// Department Class
    /// </summary>
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int DID { get; set; }
        [Required]
        public string? DName { get; set; }
        [Required]
        [Range(1, 10)]
        public int DCapacity { get; set; } = 1;

        public virtual List<Employee>? Employee { get; set; }
    }
}
