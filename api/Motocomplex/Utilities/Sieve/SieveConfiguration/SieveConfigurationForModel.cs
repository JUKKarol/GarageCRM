using Motocomplex.Entities;
using Sieve.Services;

namespace Motocomplex.Utilities.Sieve.SieveConfiguration
{
    public class SieveConfigurationForModel : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Model>(m => m.CreatedAt)
                .CanSort();

            mapper.Property<Model>(m => m.UpdatedAt)
                .CanSort();

            mapper.Property<Model>(m => m.brandId)
                .CanFilter()
                .CanSort();

            mapper.Property<Model>(m => m.Name)
                .CanFilter()
                .CanSort();
        }
    }
}
