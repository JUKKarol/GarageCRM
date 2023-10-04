using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Data.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(Guid employeeId);
        Task<List<Employee>> GetEmployees(SieveModel query);
        Task<int> GetEmployeesCount(SieveModel query);
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<Employee> AddToArchive(Guid employeeId);
        Task<Employee> BackFromArchive(Guid employeeId);
        }
}
