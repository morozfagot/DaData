using AutoMapper;
using DaData.Application.Abstractions.Messaging;
using DaData.Domain.Abstractions;
using DaData.Domain.Address;
using DaData.Domain.Address.Interfaces;

namespace DaData.Application.Address.Commands
{
    internal sealed class FullAddressCommandHandler(IRepositoryManager repositoryManager,
        IMapper mapper,
        IFullAddressFactory addressFactory)
        : ICommandHandler<FullAddressCommand, FullAddressCommandSuccess>
    {
        public async Task<Result<FullAddressCommandSuccess>> Handle(FullAddressCommand command,
            CancellationToken cancellationToken = default)
        {
            var address = mapper.Map<AddressForStandartization>(command);

            var fullAddressResult = addressFactory.AddressStandardization(address, cancellationToken);

            var result = mapper.Map<FullAddressCommandSuccess>(fullAddressResult);

            return result;
        }
    }
}
