using DaData.Domain.Abstractions;
using DaData.Domain.Address.ValueObjects;

namespace DaData.Domain.Address
{
    public sealed class FullAddress : Entity
    {
        private readonly IServiceProvider _serviceProvider;
        internal FullAddress(
            Guid id,
            Country country,
            Sity sity,
            Street street,
            int houseNamber,
            int roomNamber,
            int postalCode)
            : base(id)
        {
            Country = country;
            Sity = sity;
            Street = street;
            HouseNumber = houseNamber;
            RoomNumber = roomNamber;
            PostalCode = postalCode;
        }

        public Country Country { get; private set; }
        public Sity Sity { get; private set; }
        public Street Street { get; private set; }
        public int HouseNumber { get; private set; }
        public int RoomNumber { get; private set; }
        public int PostalCode { get; private set; }
    }
}
