﻿using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contract;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[ApiController]
[Route("api/companies/{companyId}/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _service;
    public EmployeesController(IServiceManager service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
    {
        var employees =await _service.EmployeeService.GetEmployees(companyId, trackChanges:
        false);
        return Ok(employees);
    }
    [HttpGet("{id:guid}",Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee =await _service.EmployeeService.GetEmployee(companyId, id,trackChanges: false);
        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, [FromBody] EmployeeForCreationDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForCreationDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);
        var employeeToReturn =await _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges:false);
        return CreatedAtRoute("GetEmployeeForCompany", new
        {
            companyId,
            id = employeeToReturn.Id
        },
        employeeToReturn);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
    {
       await _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges:
        false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employee)
    {
        if (employee is null)
            return BadRequest("EmployeeForUpdateDto object is null");
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

       await _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee, compTrackChanges: false, empTrackChanges: true);
        return NoContent();
    }


    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("patchDoc object sent from client is null.");

        var result =await _service.EmployeeService.GetEmployeeForPatch(companyId, id,compTrackChanges: false,empTrackChanges: true);
        patchDoc.ApplyTo(result.employeeToPatch);
        TryValidateModel(result.employeeToPatch);
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

       await _service.EmployeeService.SaveChangesForPatch(result.employeeToPatch,result.employeeEntity);
        return NoContent();
    }
}

