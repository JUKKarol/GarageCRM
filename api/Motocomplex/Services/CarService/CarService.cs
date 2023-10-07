using AutoMapper;
using Motocomplex.Data.Repositories.BrandRepository;
using Motocomplex.Data.Repositories.CarRepository;
using Motocomplex.DTOs.CarDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Services.CarService
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CarService(ICarRepository carRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<CarDisplayDto> GetCarById(Guid carId)
        {
            var car = await _carRepository.GetCarById(carId);
            return _mapper.Map<CarDisplayDto>(car);
        }

        public async Task<CarDisplayDto> GetCarByVin(string carVin)
        {
            var car = await _carRepository.GetCarByVin(carVin);
            return _mapper.Map<CarDisplayDto>(car);
        }

        public async Task<CarDisplayDto> GetCarByRegistrationNumber(string carRegistrationNumber)
        {
            var car = await _carRepository.GetCarByRegistrationNumber(carRegistrationNumber);
            return _mapper.Map<CarDisplayDto>(car);
        }

        public async Task<RespondListDto<CarDisplayDto>> GetCars(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var cars = await _carRepository.GetCars(query);
            var carsDto = _mapper.Map<List<CarDisplayDto>>(cars);

            RespondListDto<CarDisplayDto> respondListDto = new RespondListDto<CarDisplayDto>();
            respondListDto.Items = carsDto;
            respondListDto.ItemsCount = await _carRepository.GetCarsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<CarDisplayDto> CreateCar(CarCreateDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            await _carRepository.CreateCar(car);

            return _mapper.Map<CarDisplayDto>(car);
        }

        public async Task<CarDisplayDto> UpdateCar(CarUpdateDto carDto)
        {
            var car = _mapper.Map<Car>(carDto);
            await _carRepository.UpdateCar(car);

            return _mapper.Map<CarDisplayDto>(car);
        }
    }
}