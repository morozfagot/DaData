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
            int houseNamber,
            int roomNamber,
            int postalCode)
            : base(id)
        {
            Country = country;
            Region = region;
            Street = street;
            HouseNumber = houseNamber;
            RoomNumber = roomNamber;
            PostalCode = postalCode;
        }

        public Country Country { get; private set; }
        public Region Region { get; private set; }
        public Street Street { get; private set; }
        public int HouseNumber { get; private set; }
        public int RoomNumber { get; private set; }
        public int PostalCode { get; private set; }

        public Result<FullAddress> Create(CancellationToken cancellationToken,
            Country country,
            Region region,
            Street street,
            int house,
            int room,
            int postalCode)
        {
            var fullAddress = new FullAddress(Guid.NewGuid(), country, region, street, house, room, postalCode);

            fullAddress.RaiseDomainEvent(new AddressStandartizedDomainEvent(fullAddress.Id));
            return fullAddress;
        }
    }
}
