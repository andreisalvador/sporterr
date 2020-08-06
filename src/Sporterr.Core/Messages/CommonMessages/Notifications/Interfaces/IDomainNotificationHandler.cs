using MediatR;
using System.Collections.Generic;

namespace Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces
{
    public interface IDomainNotificationHandler<TNotification> where TNotification : INotification
    {
        IReadOnlyCollection<TNotification> Notifications { get; }
        bool HasNotifications();
        IEnumerable<TNotification> GetByKey(string key);
    }
}
