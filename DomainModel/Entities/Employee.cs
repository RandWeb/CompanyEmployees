namespace DomainModel.Entities;

public class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; }

    public int Age { get; set; }

    public string? Position { get; set; }

    public Guid CompanyId { get; set; }
}

