using AutoMapper;
using DaData.Application.Abstractions.Messaging;
using DaData.Application.Address.Queries.FullAddressQuery;
using DaData.Domain.Abstractions;

namespace DaData.Application.Address.Queries.FullAddress
{
    public class FullAddressQueryHandler(IMapper mapper, IDaDataService daDataService)
        : IQueryHandler<FullAddressQuery, FullAddressDto>
    {
        public async Task<Result<FullAddressDto>> Handle(FullAddressQuery query,
            CancellationToken cancellationToken = default)
        {
            var daDataResult =  await daDataService.GetAddress(query.addressForStandardization);

            if (daDataResult.IsSuccess == true)
            {
                var fullAddressDto = mapper.Map<FullAddressDto>(daDataResult.Value);

                return fullAddressDto;
            }

            return Result.Failure<FullAddressDto>(daDataResult.Error);
        }
    }
}
