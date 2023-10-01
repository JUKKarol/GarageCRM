using Motocomplex.DTOs.CarDTOs;
using Motocomplex.DTOs.SharedDTOs;
using Sieve.Models;
using System.Threading.Tasks;

namespace Motocomplex.Services.CarService
{
    public interface ICarService
    {
        Task<CarDisplayDto> GetCarById(Guid carId);
        Task<CarDisplayDto> GetCarByVin(string vin);
        Task<RespondListDto<CarDisplayDto>> GetCars(SieveModel query);
        Task<CarDisplayDto> CreateCar(CarCreateDto carDto);
        Task<CarDisplayDto> UpdateCar(CarUpdateDto carDto);
    }
}
