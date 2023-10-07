using Motocomplex.Entities;
using Sieve.Services;

namespace Motocomplex.Utilities.Sieve.SieveConfiguration
{
    public class SieveConfigurationForCustomer : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Customer>(c => c.CreatedAt)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.UpdatedAt)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.PhoneNumber)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.Email)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.Nip)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.City)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.Address)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.PostalCode)
                .CanFilter()
                .CanSort();

            mapper.Property<Customer>(c => c.IsArchive)
                .CanFilter()
                .CanSort();
        }
    }
}