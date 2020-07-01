using Raven.Client.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sporterr.Reading
{
    public class RavenDocumentStore
    {
        private readonly Lazy<IDocumentStore> LazyStore;
        public IDocumentStore Store => LazyStore.Value;

        public RavenDocumentStore()
        {
            LazyStore =  new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore
                {
                    Urls = new[] { "http://localhost:8080" },
                    Database = "SporterrDocuments"
                };



                return store.Initialize();
            });
        }
    }
}
