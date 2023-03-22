using Application.Features.Employees.Commands.CreateEmployee;
using Application.Features.Employees.Commands.DeleteEmployee;
using Application.Features.Employees.Commands.UpdateEmployee;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Models;
using Application.Features.Employees.Queries.GetByIdEmployee;
using Application.Features.Employees.Queries.GetListEmployee;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeQuery getListEmployeeQuery = new() { PageRequest = pageRequest };
        EmployeeListModel result = await Mediator.Send(getListEmployeeQuery);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdEmployeeQuery getByIdEmployeeQuery)
    {
        EmployeeDto result = await Mediator.Send(getByIdEmployeeQuery);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteEmployeeCommand deleteEmployeeCommand)
    {
        DeletedEmployeeDto result = await Mediator.Send(deleteEmployeeCommand);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        CreatedEmployeeDto result = await Mediator.Send(createEmployeeCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
    {
        UpdatedEmployeeDto result = await Mediator.Send(updateEmployeeCommand);
        return Ok(result);
    }
}