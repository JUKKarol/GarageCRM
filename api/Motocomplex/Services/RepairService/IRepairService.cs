using Motocomplex.DTOs.RepairDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Enums;
using Sieve.Models;

namespace Motocomplex.Services.RepairService
{
    public interface IRepairService
    {
        Task<RepairDisplayDto> GetRepairById(Guid repairId);

        Task<RespondListDto<RepairDisplayDto>> GetRepairs(SieveModel query);

        Task<RepairDisplayDto> CreateRepair(RepairCreateDto repairDto);

        Task<RepairDisplayDto> UpdateRepair(RepairUpdateDto repairDto);

        Task<RepairDisplayDto> ChangeRepairStatus(Guid repairId, RepairStatus status);
    }
}