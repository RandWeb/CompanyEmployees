using DomainModel.Entities;

namespace Contracts;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);
    Task<Company> GetCompany(Guid companyId, bool trackChanges);
    Task CreateCompany(Company company);
    Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
    Task DeleteCompany(Company company);
}
