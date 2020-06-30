using Sporterr.Core.Data.EventSourcing;
using Sporterr.Core.Messages;
using System.Threading.Tasks;

namespace Sporterr.EventSourcing.Repository
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly EventStoreContext _context;
        public EventSourcingRepository(EventStoreContext context)
        {
            _context = context;
        }

        public async Task SaveEvent<TEvent>(TEvent @event) where TEvent : Event
        {
            
            await _context.Events.InsertOneAsync(new EventData(@event));
        }
    }
}
