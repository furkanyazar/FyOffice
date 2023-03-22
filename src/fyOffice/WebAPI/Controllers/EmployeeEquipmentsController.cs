using Application.Features.EmployeeEquipments.Models;
using Application.Features.EmployeeEquipments.Queries.GetListEmployeeEquipmentByEmployeeId;
using Application.Features.EmployeeEquipments.Queries.GetListEmployeeEquipmentByEquipmentId;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeEquipmentsController : BaseController
{
    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetListByEmployeeId([FromRoute] int employeeId, [FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeEquipmentByEmployeeIdQuery getListEmployeeEquipmentByEmployeeIdQuery =
            new() { EmployeeId = employeeId, PageRequest = pageRequest };
        EmployeeEquipmentListModel result = await Mediator.Send(getListEmployeeEquipmentByEmployeeIdQuery);
        return Ok(result);
    }

    [HttpGet("[action]/{equipmentId}")]
    public async Task<IActionResult> GetListByEquipmentId([FromRoute] int equipmentId, [FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeEquipmentByEquipmentIdQuery getListEmployeeEquipmentByEquipmentIdQuery =
            new() { EquipmentId = equipmentId, PageRequest = pageRequest };
        EmployeeEquipmentListModel result = await Mediator.Send(getListEmployeeEquipmentByEquipmentIdQuery);
        return Ok(result);
    }
}