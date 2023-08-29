using Microsoft.EntityFrameworkCore;
using TesteApiGmillView.Models;

namespace TesteApiGmillView.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options) { }

        public DbSet<EmployeeProject> EmployeesProjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyContext).Assembly);
        }

    }
}
