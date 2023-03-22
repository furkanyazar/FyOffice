using Application.Features.Computers.Commands.CreateComputer;
using Application.Features.Computers.Commands.DeleteComputer;
using Application.Features.Computers.Commands.UpdateComputer;
using Application.Features.Computers.Dtos;
using Application.Features.Computers.Models;
using Application.Features.Computers.Queries.GetByIdComputer;
using Application.Features.Computers.Queries.GetListComputer;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ComputersController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListComputerQuery getListComputerQuery = new() { PageRequest = pageRequest };
        ComputerListModel result = await Mediator.Send(getListComputerQuery);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdComputerQuery getByIdComputerQuery)
    {
        ComputerDto result = await Mediator.Send(getByIdComputerQuery);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteComputerCommand deleteComputerCommand)
    {
        DeletedComputerDto result = await Mediator.Send(deleteComputerCommand);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateComputerCommand createComputerCommand)
    {
        CreatedComputerDto result = await Mediator.Send(createComputerCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateComputerCommand updateComputerCommand)
    {
        UpdatedComputerDto result = await Mediator.Send(updateComputerCommand);
        return Ok(result);
    }
}