using AutoMapper;
using Motocomplex.DTOs.EmployeeDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class EmployeeMappingProfile : Profile
    {
        public EmployeeMappingProfile()
        {
            CreateMap<EmployeeDetailsDto, Employee>().ReverseMap();
            CreateMap<EmployeeCreateDto, Employee>().ReverseMap();
            CreateMap<EmployeeUpdateDto, Employee>().ReverseMap();
        }
    }
}
