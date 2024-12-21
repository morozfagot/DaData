using AutoMapper;
using DaData.Domain.Address.ValueObjects;

namespace DaData.Application.Address.Queries.FullAddressQuery
{
    public record FullAddressDto(Guid id,
        Country country,
        Region region,
        Street street,
        int houseNamber,
        int roomNamber,
        int postalCode)
    {
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Domain.Address.FullAddress, FullAddressDto>();
            }
        }
    }
}
