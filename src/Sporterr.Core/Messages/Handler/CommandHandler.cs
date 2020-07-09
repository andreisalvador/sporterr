using FluentValidation.Results;
using Sporterr.Core.Communication.Mediator;
using Sporterr.Core.Data;
using Sporterr.Core.DomainObjects.Interfaces;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Messages.Handler
{
    public abstract class CommandHandler<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        private readonly IRepository<TAggregateRoot> _repository;
        private readonly IMediatrHandler _mediatr;
        protected ValidationResult _validationResult;

        public CommandHandler(IRepository<TAggregateRoot> repository, IMediatrHandler mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
            _validationResult = new ValidationResult();
        }

        protected void AddError(ValidationFailure validationFailure)
            => _validationResult.Errors.Add(validationFailure);

        protected void AddError(string errorMessage) =>
            AddError(new ValidationFailure(string.Empty, errorMessage));


        protected async Task<ValidationResult> Save()
        {
            if (!await _repository.Commit()) AddError("Occured an error while saving data.");
            return _validationResult;
        }

        protected async Task<ValidationResult> NotifyAndReturn(string message)
        {
            await _mediatr.Notify(new DomainNotification(typeof(TAggregateRoot).Name, message));
            return _validationResult;
        }

        protected async Task<ValidationResult> SaveAndPublish(params Event[] events)
        {
            ValidationResult saveResult = await Save();

            if (saveResult.IsValid)
                await PublishEvents(events);

            return saveResult;
        }

        protected async Task<ValidationResult> PublishEvents(params Event[] events)
        {
            foreach (Event @event in events)
                await _mediatr.Publish(@event);

            return _validationResult;
        }
    }
}
