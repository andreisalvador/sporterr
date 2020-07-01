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


                var eu = session.Load<object>("9d9fb91a-6b0e-4374-bdf4-ada4bec2b225");
            }

            return Task.CompletedTask;
        }
    }
}
