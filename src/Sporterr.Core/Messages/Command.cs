using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Core.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public ValidationResult ValidationResult => Validar();

        protected abstract ValidationResult Validar();

    }
}
