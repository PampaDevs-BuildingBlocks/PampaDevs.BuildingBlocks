using FluentValidation;
using MediatR;
using PampaDevs.Bus.InProc.Notifications;
using PampaDevs.Utils.Extensions;
using ReflectionMagic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PampaDevs.Bus.InProc
{
    public class CommandValidationHandler<TRequest, TRequestResponse> : IPipelineBehavior<TRequest, TRequestResponse>
        where TRequest : IRequest<TRequestResponse>        
    {
        private readonly IDomainDispatcher _dispatcher;

        public CommandValidationHandler(IDomainDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<TRequestResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TRequestResponse> next)
        {
            if (request.GetType().BaseType.IsGenericType && request.GetType().BaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(Command<,>)))
            {
                var command = request.AsDynamic();

                if(command.IsValid())
                    return await next();

                foreach (var error in command.ValidationResult.Errors)
                {
                    _dispatcher.DispatchEvent(new DomainNotification(command.MessageType, error.ErrorMessage));
                }

                return default;
            }
            else
            {
                return await next();
            }
            //if (IsValidCommand(request))
            //    return default(TRequestResponse);

        }
    }
}
