using AutoMapper;
using DaData.Domain.Address;
using DaData.Domain.Address.ValueObjects;
namespace DaData.Application.Address.Commands
{
    public record FullAddressDto(Guid id, Country country, Region region, Street street, int houseNamber, int roomNamber,
        int postalCode)
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<FullAddress, FullAddressDto>();
            }
        }
    }
}
