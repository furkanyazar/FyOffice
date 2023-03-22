using Application.Features.Equipments.Commands.CreateEquipment;
using Application.Features.Equipments.Commands.DeleteEquipment;
using Application.Features.Equipments.Commands.UpdateEquipment;
using Application.Features.Equipments.Dtos;
using Application.Features.Equipments.Queries.GetByIdEquipment;
using Application.Features.Equipments.Models;
using Application.Features.Equipments.Queries.GetListEquipment;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class EquipmentsController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEquipmentQuery getListEquipmentQuery = new() { PageRequest = pageRequest };
        EquipmentListModel result = await Mediator.Send(getListEquipmentQuery);
        return Ok(result);
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdEquipmentQuery getByIdEquipmentQuery)
    {
        EquipmentDto result = await Mediator.Send(getByIdEquipmentQuery);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteEquipmentCommand deleteEquipmentCommand)
    {
        DeletedEquipmentDto result = await Mediator.Send(deleteEquipmentCommand);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEquipmentCommand createEquipmentCommand)
    {
        CreatedEquipmentDto result = await Mediator.Send(createEquipmentCommand);
        return Created("", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEquipmentCommand updateEquipmentCommand)
    {
        UpdatedEquipmentDto result = await Mediator.Send(updateEquipmentCommand);
        return Ok(result);
    }
}