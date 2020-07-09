using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages
{
    public abstract class Command<TCommand> : Message, IRequest<ValidationResult>
    {
        private readonly IValidator _commandValidator;
        public Command(AbstractValidator<TCommand> commandValidator)
        {
            _commandValidator = commandValidator;
        }
        public ValidationResult ValidationResult { get; private set; }
        public bool IsValid()
        {            
            ValidationResult = _commandValidator.Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
