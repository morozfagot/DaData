using DaData.Domain.Abstractions;
using System.Linq.Expressions;

namespace DaData.Domain.Address.Interfaces
{
    public interface IAddressRepository : IRepositoryBase<FullAddress>
    {
        Task<IEnumerable<FullAddress>> GetAllFullAddresssAsync(CancellationToken cancellationToken = default);
        Task<FullAddress> GetFullAddressByIdAsync(int id,
            CancellationToken cancellationToken = default);
        Task<FullAddress> GetFullAddressByConditionAsync(Expression<Func<FullAddress, bool>> expression,
            CancellationToken cancellationToken = default);
        void CreateFullAddress(FullAddress FullAddress);
        void UpdateFullAddress(FullAddress FullAddress);
        void DeleteFullAddress(FullAddress FullAddress);
    }
}
