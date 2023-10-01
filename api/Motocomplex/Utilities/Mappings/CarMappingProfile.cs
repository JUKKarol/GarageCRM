using AutoMapper;
using Motocomplex.DTOs.CarDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarDisplayDto, Car>().ReverseMap();
            CreateMap<CarCreateDto, Car>().ReverseMap();
            CreateMap<CarUpdateDto, Car>().ReverseMap();
        }
    }
}
