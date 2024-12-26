using AutoMapper;
using DaData.Domain.Address;
using DaData.Domain.Address.ValueObjects;

namespace DaData.Application.Address.Queries.ParseAddresses
{
    public class FullAddressDto()
    {
        public Guid Id { get; set; }
        public string Country { get; private set; }
        public string Region { get; private set; }
        public string Street { get; private set; }
        public int HouseNumber { get; private set; }
        public int RoomNumber { get; private set; }
        public int PostalCode { get; private set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<FullAddress, FullAddressDto>();
            }
        }
    }
}
