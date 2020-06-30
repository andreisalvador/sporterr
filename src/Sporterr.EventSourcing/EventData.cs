using MongoDB.Bson;
using MongoDB.Bson.IO;
using Sporterr.Core.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sporterr.EventSourcing
{
    public class EventData
    {
        public Guid AggregateId { get; private set; }
        public string MessageType { get; private set; }
        public DateTime Timestamp { get; private set; }
        public BsonDocument Data { get; private set; }

        public EventData(Event @event)
        {
            AggregateId = @event.AggregateId;
            MessageType = @event.MessageType;
            Timestamp = @event.Timestamp;
            Data = @event.ToBsonDocument();
        }
    }
}
