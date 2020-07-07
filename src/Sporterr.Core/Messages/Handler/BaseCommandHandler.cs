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
    public abstract class BaseCommandHandler<TAggregateRoot> where TAggregateRoot : IAggregateRoot
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

        protected async Task<bool> SaveAndPublish(params Event[] events)
        {
            bool salvou = await Save();

            if (salvou)
                await PublishEvents(events);

            return salvou;
        }

        protected async Task<bool> PublishEvents(params Event[] events)
        {
            foreach (Event @event in events)
                await _mediatr.Publish(@event);

            return await Task.FromResult(true);
        }
    }
}
