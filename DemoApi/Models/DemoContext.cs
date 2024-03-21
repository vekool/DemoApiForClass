using Microsoft.EntityFrameworkCore;

namespace DemoApi.Models
{
    public class DemoContext:DbContext
    {
        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
