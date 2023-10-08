using AutoMapper;
using Motocomplex.DTOs.ModelDTOs;
using Motocomplex.DTOs.RepairDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class RepairMappingProfile : Profile
    {
        public RepairMappingProfile()
        {
            CreateMap<RepairDisplayDto, Repair>().ReverseMap();
            CreateMap<RepairCreateDto, Repair>().ReverseMap();
            CreateMap<RepairUpdateDto, Repair>().ReverseMap();
        }
    }
}
