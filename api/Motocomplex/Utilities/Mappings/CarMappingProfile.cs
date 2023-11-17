using AutoMapper;
using Motocomplex.DTOs.CarDTOs;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<Car, CarDetailsDto>().ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .AfterMap((src, dest) => dest.Model.BrandName = src.Model.Brand.Name);
            CreateMap<CarDisplayDto, Car>().ReverseMap();
            CreateMap<CarCreateDto, Car>().ReverseMap();
            CreateMap<CarUpdateDto, Car>().ReverseMap();
        }
    }
}