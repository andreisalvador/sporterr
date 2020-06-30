using EventStore.ClientAPI;
using Newtonsoft.Json;
using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Messages;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Sporterr.EventSourcing.Repository
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        public async Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            await EventStore.GetConnection().AppendToStreamAsync(@event.AggregateId.ToString(), ExpectedVersion.Any, new EventData[] {
                new EventData(Guid.NewGuid(),
                              @event.MessageType,
                              true,
                              Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)),
                              null)
            });
        }
    }
}
