﻿namespace Shared.DataTransferObjects;
public record CompanyDto {
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? FullAddress { get; init; }
};

public record CompanyForUpdateDto(string Name, string Address, string Country,IEnumerable<EmployeeForCreationDto> Employees);