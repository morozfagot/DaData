using AutoMapper;
using DaData.Application.Abstractions.Messaging;
using DaData.Domain.Address;

namespace DaData.Application.Address.Commands
{
    public record FullAddressCommand(Guid Id, string addressForStandardization) : ICommand<FullAddressDto>
    {
    }
}
