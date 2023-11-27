using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.CarRepository
{
    public class CarRepository(
        MotocomplexContext _db,
        ISieveProcessor _sieveProcessor) : ICarRepository
    {
        public async Task<Car> GetCarById(Guid carId)
        {
            var car = await _db.Cars.Include(c => c.Model)
                .ThenInclude(m => m.Brand)
                .FirstOrDefaultAsync(c => c.Id == carId);

            return car;
        }

        public async Task<Car> GetCarByVin(string carVin)
        {
            return await _db.Cars.FirstOrDefaultAsync(c => c.Vin == carVin);
        }

        public async Task<Car> GetCarByRegistrationNumber(string carRegistrationNumber)
        {
            return await _db.Cars.FirstOrDefaultAsync(c => c.RegistrationNumber == carRegistrationNumber);
        }

        public async Task<List<Car>> GetCars(SieveModel query)
        {
            var cars = _db
                .Cars
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, cars)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCarsCount(SieveModel query)
        {
            var cars = _db
                .Cars
                .AsNoTracking()
                .AsQueryable();

            return await _sieveProcessor
                .Apply(query, cars, applyPagination: false)
                .CountAsync();
        }

        public async Task<Car> CreateCar(Car car)
        {
            await _db.Cars.AddAsync(car);
            await _db.SaveChangesAsync();

            return car;
        }

        public async Task<Car> UpdateCar(Car updatedCar)
        {
            var car = await _db.Cars.FirstOrDefaultAsync(c => c.Id == updatedCar.Id);

            var carCraetedAt = car.CreatedAt;
            updatedCar.CreatedAt = carCraetedAt;

            var entry = _db.Entry(car);
            entry.CurrentValues.SetValues(updatedCar);

            await _db.SaveChangesAsync();

            return car;
        }
    }
}