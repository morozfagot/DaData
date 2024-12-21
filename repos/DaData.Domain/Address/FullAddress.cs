using DaData.Domain.Abstractions;
using DaData.Domain.Address.Events;
using DaData.Domain.Address.ValueObjects;

namespace DaData.Domain.Address
{
    public sealed class FullAddress : Entity
    {
        private FullAddress(
            Guid id,
            Country country,
            Region region,
            Street street,
            int houseNumber,
            int roomNumber,
            int postalCode)
            : base(id)
        {
            Country = country;
            Region = region;
            Street = street;
            HouseNumber = houseNumber;
            RoomNumber = roomNumber;
            PostalCode = postalCode;
        }

        public Country Country { get; private set; }
        public Region Region { get; private set; }
        public Street Street { get; private set; }
        public int HouseNumber { get; private set; }
        public int RoomNumber { get; private set; }
        public int PostalCode { get; private set; }

        public static Result<FullAddress> Create(string country,
            string region,
            string street,
            int houseNumber,
            int roomNumber,
            int postalCode)
        {
            var address = new FullAddress(
                Guid.NewGuid(),
                new Country(country),
                new Region(region),
                new Street(street),
                houseNumber,
                roomNumber,
                postalCode
            );

            address.RaiseDomainEvent(new AddressStandartizedDomainEvent(address.Id));

            return address;
        }
    }
}
