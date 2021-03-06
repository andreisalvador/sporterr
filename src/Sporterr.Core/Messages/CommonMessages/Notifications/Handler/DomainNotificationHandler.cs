﻿using MediatR;
using Sporterr.Core.Messages.CommonMessages.Notifications.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sporterr.Core.Messages.CommonMessages.Notifications.Handler
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>, IDomainNotificationHandler<DomainNotification>
    {
        private readonly List<DomainNotification> _notifications;
        public IReadOnlyCollection<DomainNotification> Notifications => _notifications.AsReadOnly();

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public IEnumerable<DomainNotification> GetByKey(string key)
        {
            return _notifications.Where(n => n.Key.Equals(key));
        }
        public bool HasNotifications() => _notifications.Count > 0;

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }
    }
}
