using DaData.Application.Abstractions.Messaging;
using System.ComponentModel;

namespace DaData.Application.Address.Queries.ParseAddresses
{
    public record FullAddressQuery([property: DefaultValue("москва дмитровское шоссе 70 178")] string addressForStandardization) : IQuery<FullAddressDto>
    {
    }
}
