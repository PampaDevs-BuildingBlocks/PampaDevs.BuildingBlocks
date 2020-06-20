using FluentValidation;
using MediatR;
using PampaDevs.Bus.InProc.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace PampaDevs.Bus.InProc
{
    public class CommandValidationHandler<TCommand, TCommandValidator, TCommandResponse> : IPipelineBehavior<TCommand, TCommandResponse>
        where TCommand : Command<TCommandValidator, TCommandResponse>
        where TCommandValidator : IValidator, new()
    {
        private readonly IDomainDispatcher _dispatcher;

        public CommandValidationHandler(IDomainDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<TCommandResponse> Handle(TCommand command, CancellationToken cancellationToken, RequestHandlerDelegate<TCommandResponse> next)
        {
            if (IsValidCommand(command))
                return default(TCommandResponse);

            var response = await next();

            return response;
        }

        private bool IsValidCommand(TCommand command)
        {
            if (command.IsValid()) return true;

            foreach (var error in command.ValidationResult.Errors)
            {
                _dispatcher.DispatchEvent(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
