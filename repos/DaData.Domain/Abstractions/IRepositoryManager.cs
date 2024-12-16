using DaData.Domain.Address.Interfaces;

namespace DaData.Domain.Abstractions
{
    public interface IRepositoryManager
    {
        IAddressRepository AddressRepository { get; }
        IUnitOfWork UnitOfWork { get; }
    }
}
