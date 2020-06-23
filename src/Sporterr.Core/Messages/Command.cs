using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages
{
    public abstract class Command<TCommand> : Message, IRequest<bool>
    {
        public ValidationResult ValidationResult { get; private set; }

        protected abstract AbstractValidator<TCommand> GetValidator();
        public bool IsValid()
        {
            IValidator<TCommand> validator = GetValidator();
            ValidationResult = validator.Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
