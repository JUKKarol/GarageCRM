using Microsoft.EntityFrameworkCore;
using Motocomplex.Entities;
using Sieve.Models;
using Sieve.Services;

namespace Motocomplex.Data.Repositories.CarRepository
{
    public class CarRepository : ICarRepository
    {
        private readonly MotocomplexContext _db;
        private readonly ISieveProcessor _sieveProcessor;

        public CarRepository(MotocomplexContext db, ISieveProcessor sieveProcessor)
        {
            _db = db;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<Car> GetCarById(Guid carId)
        {
            return await _db.Cars.FirstOrDefaultAsync(c => c.Id == carId);
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