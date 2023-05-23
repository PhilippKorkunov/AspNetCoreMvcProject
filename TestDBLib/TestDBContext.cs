using Microsoft.EntityFrameworkCore;
using TestDBLib.Entities;

namespace TestDBLib
{
    public class TestDBContext : DbContext
    {
        public DbSet<Department> Department { get; set; } = null!;
        public DbSet<Employee> Employee { get; set; } = null!;

       /* public TestDBContext()
        {

        }*/
        public TestDBContext(DbContextOptions<TestDBContext> options) : base(options)
        { }

      /*  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=GENIUS\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True; TrustServerCertificate=True");
        }*/

    }
}
