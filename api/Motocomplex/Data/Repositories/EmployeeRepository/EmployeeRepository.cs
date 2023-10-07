using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MotocomplexContext _db;
        private readonly ISieveProcessor _sieveProcessor;

        public EmployeeRepository(MotocomplexContext db, ISieveProcessor sieveProcessor)
        {
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Employee> GetEmployeeById(Guid employeeId)
        {
            return await _db.Employee.FirstOrDefaultAsync(e => e.Id == employeeId);
        }

        public async Task<List<Employee>> GetEmployees(SieveModel query)
        {
            var employees = _db
                .Employee
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, employees)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetEmployeesCount(SieveModel query)
        {
            var employees = _db
                .Employee
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, employees, applyPagination: false)
                .CountAsync();
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            await _db.Employee.AddAsync(employee);
            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee updatedEmployee)
        {
            var employee = await _db.Employee.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

            var employeeCraetedAt = employee.CreatedAt;
            var employeeIsArchive = employee.IsArchive;
            updatedEmployee.CreatedAt = employeeCraetedAt;
            updatedEmployee.IsArchive = employeeIsArchive;

            var entry = _db.Entry(employee);
            entry.CurrentValues.SetValues(updatedEmployee);

            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> AddToArchive(Guid employeeId)
        {
            var employee = await _db.Employee.FirstOrDefaultAsync(e => e.Id == employeeId);
            employee.IsArchive = true;
            await _db.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> BackFromArchive(Guid employeeId)
        {
            var employee = await _db.Employee.FirstOrDefaultAsync(e => e.Id == employeeId);
            employee.IsArchive = false;
            await _db.SaveChangesAsync();

            return employee;
        }
    }
}