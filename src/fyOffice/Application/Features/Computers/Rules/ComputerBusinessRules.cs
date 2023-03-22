using Application.Features.Computers.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Computers.Rules;

public class ComputerBusinessRules : BaseBusinessRules
{
    private readonly IComputerRepository _computerRepository;

    public ComputerBusinessRules(IComputerRepository computerRepository)
    {
        _computerRepository = computerRepository;
    }

    public void ComputerIdShouldExistWhenSelected(Computer? computer)
    {
        if (computer is null)
            throw new BusinessException(ComputerMessages.ComputerNotExists);
    }

    public async Task ComputerShouldNotHasEmployeeWhenDeleted(int id)
    {
        Computer? result = await _computerRepository.GetAsync(c => c.Id == id, enableTracking: false);
        if (result?.EmployeeId is not null)
            throw new BusinessException(ComputerMessages.ComputerHasEmployee);
    }

    public async Task ComputerEmployeeIdCanNotBeDuplicatedWhenInserted(int? employeeId)
    {
        Computer? result = await _computerRepository.GetAsync(c => c.EmployeeId == employeeId);
        if (result?.EmployeeId is not null)
            throw new BusinessException(ComputerMessages.ComputerAlreadyHasEmployee);
    }

    public async Task ComputerEmployeeIdCanNotBeDuplicatedWhenUpdated(int id, int? employeeId)
    {
        Computer? result = await _computerRepository.GetAsync(c => c.Id != id && c.EmployeeId == employeeId,
                                                              enableTracking: false);
        if (result?.EmployeeId is not null)
            throw new BusinessException(ComputerMessages.ComputerAlreadyHasEmployee);
    }
}