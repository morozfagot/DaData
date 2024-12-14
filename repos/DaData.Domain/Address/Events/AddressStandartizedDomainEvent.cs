using DaData.Domain.Abstractions;

namespace DaData.Domain.Address.Events
{
    public sealed record AddressStandartizedDomainEvent(Guid AddressId) : IDomainEvent
    {
    }
}
