using DaData.Domain.Abstractions;
using MediatR;

namespace DaData.Application.Abstractions.Messaging
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<TCommand, TResponce> : IRequestHandler<TCommand, Result<TResponce>>
        where TCommand : ICommand<TResponce>
    {
    }
}
