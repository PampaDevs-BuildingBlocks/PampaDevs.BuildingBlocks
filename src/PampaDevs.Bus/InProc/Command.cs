using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PampaDevs.Bus.InProc
{
    public abstract class Command<TCommandValidator, TCommandResponse> : Message, ICommand<TCommandValidator>, IRequest<TCommandResponse>
        where TCommandValidator : IValidator, new()
    {
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            ValidationResult = new TCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
