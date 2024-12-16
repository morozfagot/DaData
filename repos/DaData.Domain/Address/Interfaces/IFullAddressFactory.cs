using DaData.Domain.Abstractions;

namespace DaData.Domain.Address.Interfaces
{
    public interface IFullAddressFactory
    {
        Task<Result<FullAddress>> AddressStandardization(AddressForStandartization addressForStandartization,
            CancellationToken cancellationToken);
    }
}
