using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces
{
    public interface INotificationHandler<TNotification> : MediatR.INotificationHandler<TNotification> where TNotification : INotification
    {
        IReadOnlyCollection<TNotification> Notifications { get; }
        bool HasNotifications();
        IEnumerable<TNotification> GetByKey(string key);
    }
}
