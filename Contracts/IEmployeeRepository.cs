using DomainModel.Entities;

namespace Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees(Guid companyId, bool trackChanges);
    Task<Employee> GetEmployee(Guid companyId, Guid id, bool trackChanges);

    Task CreateEmployeeForCompany(Guid companyId, Employee employee);

    Task DeleteEmployee(Employee employee);
}
