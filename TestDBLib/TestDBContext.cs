using Microsoft.EntityFrameworkCore;
using TestDBLib.Entities;

namespace TestDBLib
{
    public class TestDBContext : DbContext
    {
        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<Employee> Employee { get; set; } = null!;

        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        { }

    }
}
