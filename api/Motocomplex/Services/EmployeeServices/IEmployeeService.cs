using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;

namespace Motocomplex.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<EmployeeDetailsDto> GetEmployeeById(Guid employeeId);
        Task<RespondListDto<EmployeeDetailsDto>> GetEmployees(SieveModel query);
        Task<EmployeeDetailsDto> CreateEmployee(EmployeeCreateDto employeeDto);
        Task<EmployeeDetailsDto> UpdateEmployee(EmployeeUpdateDto employeeDto);
        Task<bool> CheckIsEmployeeInArchive(Guid employeeId);
        Task ChangeEmployeeArchiveBool(Guid employeeId, bool isArchive);
    }
}
