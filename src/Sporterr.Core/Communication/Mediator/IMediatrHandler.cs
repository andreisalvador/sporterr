using Sporterr.Core.Messages;
using Sporterr.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Communication.Mediator
{
    public interface IMediatrHandler
    {
        Task Publish<TEvent>(TEvent evento) where TEvent : Event;
        Task<bool> Send<TEvent>(TEvent evento) where TEvent : Command;
        Task Notify<TNotification>(TNotification notification) where TNotification : DomainNotification;
    }
}
