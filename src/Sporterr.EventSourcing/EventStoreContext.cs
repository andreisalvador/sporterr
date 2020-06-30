using MongoDB.Driver;
using Sporterr.Core.Messages;

namespace Sporterr.EventSourcing
{
    public class EventStoreContext
    {
        private readonly IMongoDatabase _context;
        public EventStoreContext()
        {
            IMongoClient client = new MongoClient("mongodb://andrei.salvador:mongopass@localhost:27017");
            _context = client.GetDatabase("event-store");
        }

        public IMongoCollection<EventData> Events { get => _context.GetCollection<EventData>("events"); }
    }
}
