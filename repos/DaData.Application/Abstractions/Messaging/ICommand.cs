using DaData.Domain.Abstractions;
using MediatR;

namespace DaData.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponce> : IRequest<Result<TResponce>>, IBaseCommand
    {
    }

    public interface IBaseCommand
    {
    }
}
