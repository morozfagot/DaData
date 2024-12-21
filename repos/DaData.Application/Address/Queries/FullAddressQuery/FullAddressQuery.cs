using DaData.Application.Abstractions.Messaging;
using DaData.Application.Address.Queries.FullAddressQuery;

namespace DaData.Application.Address.Queries.FullAddress
{
    public record FullAddressQuery(Guid Id, string addressForStandardization) : IQuery<FullAddressDto>
    {
    }
}
