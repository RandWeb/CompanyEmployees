using DomainModel.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Mappings;

namespace Repository.context;
public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Enum?>().HaveConversion<string?>();
    }

    public DbSet<Employee>? Employees { get; set; }
    public DbSet<Company>? Companies { get; set; }
}
