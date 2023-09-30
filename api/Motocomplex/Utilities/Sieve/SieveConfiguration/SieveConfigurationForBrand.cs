using Motocomplex.Entities;
using Sieve.Services;

namespace Motocomplex.Utilities.Sieve.SieveConfiguration
{
    public class SieveConfigurationForBrand : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Brand>(b => b.CreatedAt)
              .CanSort();

            mapper.Property<Brand>(b => b.UpdatedAt)
              .CanSort();

            mapper.Property<Brand>(b => b.Name)
              .CanFilter()
              .CanSort();
        }
    }
}
