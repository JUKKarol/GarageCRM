using Motocomplex.Entities;
using Sieve.Services;

namespace Motocomplex.Utilities.Sieve.SieveConfiguration
{
    public class SieveConfigurationForEmployee : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Employee>(e => e.CreatedAt)
                .CanSort();

            mapper.Property<Employee>(e => e.UpdatedAt)
                .CanSort();

            mapper.Property<Employee>(e => e.Name)
               .CanFilter()
               .CanSort();

            mapper.Property<Employee>(e => e.Surname)
               .CanFilter()
               .CanSort();

            mapper.Property<Employee>(e => e.DateOfEmployment)
               .CanSort();

            mapper.Property<Employee>(e => e.Role)
               .CanFilter()
               .CanSort();

            mapper.Property<Customer>(c => c.IsArchive)
                .CanFilter()
                .CanSort();
        }
    }
}
