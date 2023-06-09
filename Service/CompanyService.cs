﻿using AutoMapper;
using Contracts;
using DomainModel.Entities;
using DomainModel.ErrorModel;
using Service.Contract;
using Shared.DataTransferObjects;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CompanyService(IRepositoryManager repository, ILoggerManager
    logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompanies(bool trackChanges)
    {
        //try
        //{
        var companies = await _repository.Company.GetAllCompanies(trackChanges);
        /*var companiesDto = companies.Select(c =>
                                                  new CompanyDto(c.Id,
                                                                 c.Name ?? "",
                                                                 string.Join(' ',c.Address, c.Country)))
                                                                 .ToList();*/
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
        //    throw;
        //}
    }

    public async Task<CompanyDto> GetCompany(Guid id, bool trackChanges)
    {
        var company = await _repository.Company.GetCompany(id, trackChanges);
        //Check if the company is null
        if (company is null)
            throw new CompanyNotFoundException(id);
        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }

    public async Task<CompanyDto> CreateCompany(CompanyForCreationDto company)
    {
        var companyEntity = _mapper.Map<Company>(company);
        await _repository.Company.CreateCompany(companyEntity);
        await _repository.SaveAsync();
        var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
        return companyToReturn;
    }

    public async Task<IEnumerable<CompanyDto>> GetByIds(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var companyEntities = await _repository.Company.GetByIds(ids, trackChanges);
        if (ids.Count() != companyEntities.Count())
            throw new CollectionByIdsBadRequestException();
        var companiesToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        return companiesToReturn;
    }

    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if (companyCollection is null)
            throw new CompanyCollectionBadRequest();

        var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

        foreach (var company in companyEntities)
        {
            await _repository.Company.CreateCompany(company);
        }

        await _repository.SaveAsync();
        var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
        var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
        return (companies: companyCollectionToReturn, ids: ids);
    }

    public async Task DeleteCompany(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompany(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
        await _repository.Company.DeleteCompany(company);
        await _repository.SaveAsync();
    }

    public async Task UpdateCompany(Guid companyId, CompanyForUpdateDto companyForUpdate, bool trackChanges)
    {
        var companyEntity = await _repository.Company.GetCompany(companyId, trackChanges);
        if (companyEntity is null)
            throw new CompanyNotFoundException(companyId);
        _mapper.Map(companyForUpdate, companyEntity);
        await _repository.SaveAsync();
    }
}