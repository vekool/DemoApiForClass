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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Department>().HasData(
        //        new Department { DID = 1, DName = "IT", DCapacity = 4 },
        //        new Department { DID = 2, DName = "HR", DCapacity = 2 },
        //        new Department { DID = 3, DName = "Accounts", DCapacity = 3 }
        //    );
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
