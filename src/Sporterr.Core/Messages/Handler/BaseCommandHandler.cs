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
    public class BaseCommandHandler<TAggregateRoot> where TAggregateRoot : IAggregateRoot
    {
        private readonly IRepository<TAggregateRoot> _repository;
        private readonly IMediatrHandler _mediatr;

        public BaseCommandHandler(IRepository<TAggregateRoot> repository, IMediatrHandler mediatr)
        {
            _repository = repository;
            _mediatr = mediatr;
        }

        protected async Task<bool> Save() => await _repository.Commit();

        protected async Task<bool> NotifyAndReturn(string message)
        {
            await _mediatr.Notify(new DomainNotification(typeof(TAggregateRoot).Name, message));
            return false;
        }

        protected async Task<bool> SaveAndPublish<TEvent>(TEvent @event) where TEvent : Event
        {
            bool salvou = await Save();

            if (salvou) await _mediatr.Publish<TEvent>(@event);

            return salvou;
        }
    }
}
