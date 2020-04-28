using Raven.Client.Documents;
using ReclameAquiDoTrader.Data.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReclameAquiDoTrader.Data.Extensions
{
    public static class RavenExtensions
    {
        public static void Inicializar(this IDocumentStore store)
        {
            EnsureExists(store);
            IndexesSetup.Execute(store);
        }
        private static IDocumentStore EnsureExists(IDocumentStore store)
        {
            try
            {
                using (var dbSession = store.OpenSession())
                {
                    dbSession.Query<dynamic>().Take(0).ToList();
                }
            }
            catch (Raven.Client.Exceptions.Database.DatabaseDoesNotExistException)
            {
                store.Maintenance.Server.Send(new Raven.Client.ServerWide.Operations.CreateDatabaseOperation(new Raven.Client.ServerWide.DatabaseRecord
                {
                    DatabaseName = store.Database
                }));
            }
            return store;
        }
    }
}
