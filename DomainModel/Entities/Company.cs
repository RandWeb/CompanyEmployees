namespace DomainModel.Entities;
public class Company
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}

