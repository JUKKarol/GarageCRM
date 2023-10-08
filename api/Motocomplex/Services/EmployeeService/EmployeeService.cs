using AutoMapper;
using Motocomplex.Data.Repositories.EmployeeRepository;
using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDetailsDto> GetEmployeeById(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);
            return _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public async Task<RespondListDto<EmployeeDetailsDto>> GetEmployees(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var employees = await _employeeRepository.GetEmployees(query);
            var employeesDto = _mapper.Map<List<EmployeeDetailsDto>>(employees);

            RespondListDto<EmployeeDetailsDto> respondListDto = new RespondListDto<EmployeeDetailsDto>();
            respondListDto.Items = employeesDto;
            respondListDto.ItemsCount = await _employeeRepository.GetEmployeesCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<EmployeeDetailsDto> CreateEmployee(EmployeeCreateDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.CreateEmployee(employee);

            return _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public async Task<EmployeeDetailsDto> UpdateEmployee(EmployeeUpdateDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.UpdateEmployee(employee);

            return _mapper.Map<EmployeeDetailsDto>(employee);
        }

        public async Task<bool> CheckIsEmployeeInArchive(Guid employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeById(employeeId);

            if (employee.IsArchive == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task ChangeEmployeeArchiveBool(Guid employeeId, bool isArchive)
        {
            if (isArchive == true)
            {
                await _employeeRepository.AddToArchive(employeeId);
            }
            else
            {
                await _employeeRepository.BackFromArchive(employeeId);
            }
        }
    }
}