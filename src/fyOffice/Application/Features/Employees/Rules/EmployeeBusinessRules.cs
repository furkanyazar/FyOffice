using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employees.Rules;

public class EmployeeBusinessRules : BaseBusinessRules
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeBusinessRules(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public void EmployeeIdShouldExistWhenSelected(Employee? employee)
    {
        if (employee is null)
            throw new BusinessException(EmployeeMessages.EmployeeNotExists);
    }

    public async Task EmployeeShouldNotHasComputerWhenDeleted(int id)
    {
        Employee? result = await _employeeRepository.GetAsync(c => c.Id == id, include: c => c.Include(c => c.Computer),
                                                              enableTracking: false);
        if (result?.Computer is not null)
            throw new BusinessException(EmployeeMessages.EmployeeHasComputer);
    }

    public async Task EmployeePhoneNumberCanNotBeDuplicatedWhenInserted(string? phoneNumber)
    {
        Employee? result = await _employeeRepository.GetAsync(c => c.PhoneNumber == phoneNumber);
        if (!string.IsNullOrEmpty(result?.PhoneNumber))
            throw new BusinessException(EmployeeMessages.EmployeePhoneNumberAlreadyExists);
    }

    public async Task EmployeePhoneNumberCanNotBeDuplicatedWhenUpdated(int id, string? phoneNumber)
    {
        Employee? result = await _employeeRepository.GetAsync(c => c.Id != id && c.PhoneNumber == phoneNumber,
                                                              enableTracking: false);
        if (!string.IsNullOrEmpty(result?.PhoneNumber))
            throw new BusinessException(EmployeeMessages.EmployeePhoneNumberAlreadyExists);
    }
}