using AutoMapper;
using Motocomplex.Data.Repositories.EmployeeRepository;
using Motocomplex.Data.Repositories.RepairRepository;
using Motocomplex.DTOs.RepairDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Motocomplex.Enums;
using Motocomplex.Services.EmployeeServices;
using Sieve.Models;

namespace Motocomplex.Services.RepairService
{
    public class RepairService(IEmployeeService _employeeService, IRepairRepository _repairRepository, IEmployeeRepository _employeeRepository, IMapper _mapper) : IRepairService
    {
        public async Task<RepairDisplayDto> GetRepairById(Guid repairId)
        {
            var repair = await _repairRepository.GetRepairById(repairId);
            return _mapper.Map<RepairDisplayDto>(repair);
        }

        public async Task<RespondListDto<RepairDisplayDto>> GetRepairs(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var repairs = await _repairRepository.GetRepairs(query);
            var repairsDto = _mapper.Map<List<RepairDisplayDto>>(repairs);

            RespondListDto<RepairDisplayDto> respondListDto = new RespondListDto<RepairDisplayDto>();
            respondListDto.Items = repairsDto;
            respondListDto.ItemsCount = await _repairRepository.GetRepairsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<RepairDisplayDto> CreateRepair(RepairCreateDto repairDto)
        {
            var repair = _mapper.Map<Repair>(repairDto);
            repair.Employees = await _employeeService.GetEmployeesByIds(repairDto.EmployeesIds);

            await _repairRepository.CreateRepair(repair);

            var repairResponse = _mapper.Map<RepairDisplayDto>(repair);
            repairResponse.EmployeesIds = repairDto.EmployeesIds;

            return repairResponse;
        }

        public async Task<RepairDisplayDto> UpdateRepair(RepairUpdateDto repairDto)
        {
            var repair = _mapper.Map<Repair>(repairDto);
            repair.Employees = await _employeeService.GetEmployeesByIds(repairDto.EmployeesIds);

            await _repairRepository.UpdateRepair(repair);

            var repairResponse = _mapper.Map<RepairDisplayDto>(repair);
            repairResponse.EmployeesIds = repairDto.EmployeesIds;

            return repairResponse;
        }

        public async Task<RepairDisplayDto> ChangeRepairStatus(Guid repairId, RepairStatus status)
        {
            var repair = await _repairRepository.UpdateRepairStatus(repairId, status);

            return _mapper.Map<RepairDisplayDto>(repair);
        }
    }
}