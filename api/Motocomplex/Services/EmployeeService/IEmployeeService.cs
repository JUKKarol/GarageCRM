using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDetailsDto> GetEmployeeById(Guid employeeId);

        Task<RespondListDto<EmployeeDetailsDto>> GetEmployees(SieveModel query);

        Task<List<Employee>> GetEmployeesByIds(List<Guid> employeeIds);

        Task<bool> CheckIsAllEmployeesExist(List<Guid> employeesIds);

        Task<EmployeeDetailsDto> CreateEmployee(EmployeeCreateDto employeeDto);

        Task<EmployeeDetailsDto> UpdateEmployee(EmployeeUpdateDto employeeDto);

        Task<bool> CheckIsEmployeeInArchive(Guid employeeId);

        Task ChangeEmployeeArchiveBool(Guid employeeId, bool isArchive);
    }
}