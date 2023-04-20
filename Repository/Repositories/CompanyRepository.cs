using Contracts;
using DomainModel.Entities;
using Repository.context;

namespace Repository.Repositories;
public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext)
    : base(repositoryContext)
    { }

    public async Task CreateCompany(Company company)
    {
        Create(company);
    }

    public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) =>
    FindAll(trackChanges)
    .OrderBy(c => c.Name)
    .ToList();

    public async Task<Company> GetCompany(Guid companyId, bool trackChanges) => FindByCondition(c => c.Id.Equals(companyId), trackChanges).SingleOrDefault();

    public async Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.Id), trackChanges).ToList();

    public async Task DeleteCompany(Company company) => Delete(company);


}