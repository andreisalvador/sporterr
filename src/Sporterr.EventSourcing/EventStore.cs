using EventStore.ClientAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.EventSourcing
{
    public static class EventStore
    {
        private static IEventStoreConnection _connection;

        public static IEventStoreConnection GetConnection()
        {
            _connection ??= EventStoreConnection.Create("ConnectTo=tcp://admin:changeit@localhost:1113; HeartBeatTimeout=500");

            return _connection;
        }
    }
}
