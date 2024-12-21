using DaData.Domain.Address;

namespace DaData.Domain.Abstractions
{
    public interface IDaDataService
    {
        Task<Result<FullAddress>> GetAddress(string addressForStandardization);
    }
}
