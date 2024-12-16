using DaData.Application.Abstractions.Messaging;

namespace DaData.Application.Address.Commands
{
    public record FullAddressCommand(Guid Id, string addressForStandardization) : ICommand<FullAddressDto>
    {
    }
}
