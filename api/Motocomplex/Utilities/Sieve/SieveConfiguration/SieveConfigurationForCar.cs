using Motocomplex.Entities;
using Sieve.Services;

namespace Motocomplex.Utilities.Sieve.SieveConfiguration
{
    public class SieveConfigurationForCar : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Car>(c => c.Engine)
                .CanFilter()
                .CanSort();

            mapper.Property<Car>(c => c.RegistrationNumber)
                .CanFilter()
                .CanSort();

            mapper.Property<Car>(c => c.Vin)
                .CanFilter()
                .CanSort();

            mapper.Property<Car>(c => c.yearOfProduction)
                .CanFilter()
                .CanSort();

            mapper.Property<Car>(c => c.ModelId)
                .CanFilter()
                .CanSort();
        }
    }
}