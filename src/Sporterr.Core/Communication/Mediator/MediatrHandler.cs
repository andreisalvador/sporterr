using MediatR;
using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Sporterr.Core.Communication.Mediator
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;
        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Notify<TNotification>(TNotification notification) where TNotification : DomainNotification
            => await _mediator.Publish(notification);

        public async Task Publish<TEvent>(TEvent @event) where TEvent : Event
            => await _mediator.Publish(@event);

        public async Task<bool> Send<TCommand>(TCommand command) where TCommand : Command<TCommand>
            => await _mediator.Send(command);
    }
}
