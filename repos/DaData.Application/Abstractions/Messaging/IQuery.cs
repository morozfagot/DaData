using DaData.Domain.Abstractions;
using MediatR;

namespace DaData.Application.Abstractions.Messaging
{
    public interface IQuery<Tresponse> : IRequest<Result<Tresponse>>
    {
    }
}
