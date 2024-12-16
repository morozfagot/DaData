using AutoMapper;
using DaData.Domain.Address;

namespace DaData.Application.Address.Commands
{
    internal class FullAddressDto
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
