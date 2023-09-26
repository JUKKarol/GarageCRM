using AutoMapper;
using Motocomplex.DTOs.CustomerDtos;
using Motocomplex.Entities;

namespace Motocomplex.Utilities.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerDetailsDto, Customer>().ReverseMap();
            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        }
    }
}
