using MediatR;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Sporterr.Core.Communication.Mediator
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;
        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
            //_eventSourcingRepository = eventSourcingRepository;
        }

        public async Task Notify<TNotification>(TNotification notification) where TNotification : DomainNotification
            => await _mediator.Publish(notification);

        public async Task Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            await _mediator.Publish(@event);
            //await _eventSourcingRepository.SaveEvent(@event);
        }

        public async Task<bool> Send<TCommand>(TCommand command) where TCommand : Command<TCommand>
            => await _mediator.Send(command);
    }
}
