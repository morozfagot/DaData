using DaData.Domain.Abstractions;
using MediatR;

namespace DaData.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery, TResponce> : IRequestHandler<TQuery, Result<TResponce>>
        where TQuery : IQuery<TResponce>
    {
    }
}
