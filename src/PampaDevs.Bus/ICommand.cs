using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PampaDevs.Bus
{
    public interface ICommand<TCommandValidator> : IMessage
        where TCommandValidator : IValidator, new()
    {        
        ValidationResult ValidationResult { get; }
        bool IsValid();
    }
}
