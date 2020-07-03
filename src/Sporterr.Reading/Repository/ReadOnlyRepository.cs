using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sporterr.Core.Data.Reading;
using System;
using System.Threading.Tasks;

namespace Sporterr.Reading.Repository
{
    public class ReadOnlyRepository : IReadOnlyRepository
    {
        private readonly RavenDocumentStore _ravenDocumentStore;
        public ReadOnlyRepository(RavenDocumentStore ravenDocumentStore)
        {
            _ravenDocumentStore = ravenDocumentStore;
        }

        public Task InsertOrUpdate()
        {
            using (IDocumentSession session = _ravenDocumentStore.Store.OpenSession())
            {   
                session.Store(new 
                {
                    Nome = "Andrei",
                    Idade = 24,
                    Sexo = 'M'
                });

                session.SaveChanges();

                var eu = session.Load<object>("1be6a164-b967-4063-96e5-2587fd7a3e18");
            }

            return Task.CompletedTask;
        }
    }
}
