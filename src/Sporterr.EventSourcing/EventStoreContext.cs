using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Sporterr.Core.Messages;

namespace Sporterr.EventSourcing
{
    public class EventStoreContext
    {
        private readonly IMongoDatabase _context;
        public EventStoreContext(IConfiguration configuration)
        {            
            IMongoClient client = new MongoClient(configuration.GetConnectionString("DefaultEventStoreConnection"));
            _context = client.GetDatabase("event-store");
        }

        public IMongoCollection<EventData> Events { get => _context.GetCollection<EventData>("events"); }
    }
}
