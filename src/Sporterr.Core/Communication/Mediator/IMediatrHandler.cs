using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Sporterr.Core.Communication.Mediator
{
    public interface IMediatrHandler
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : Event;
        Task<bool> Send<TCommand>(TCommand command) where TCommand : Command<TCommand>;
        Task Notify<TNotification>(TNotification notification) where TNotification : DomainNotification;
    }
}
