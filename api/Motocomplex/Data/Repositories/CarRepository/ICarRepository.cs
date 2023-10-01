﻿using Motocomplex.Entities;
using Sieve.Models;

namespace Motocomplex.Data.Repositories.CarRepository
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(Guid carId);
        Task<Car> GetCarByVin(string carVin);
        Task<List<Car>> GetCars(SieveModel query);
        Task<int> GetCarsCount(SieveModel query);
        Task<Car> CreateCar(Car car);
        Task<Car> UpdateCar(Car updatedCar);
    }
}
