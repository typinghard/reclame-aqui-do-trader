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

        //public static void Seed(IDocumentStore store)
        //{
        //    using (var session = store.OpenSession())
        //    {
        //        var parceiros = session.Query<Usu>().ToList();
        //        if (!parceiros.Any(p => p.Id == CodigosParceiros.Bndes))
        //        {
        //            var parceiroBndes = new Parceiro(CodigosParceiros.Bndes)
        //            {
        //                LandingPageCadatral = LandingPages.Bndes,
        //                Nome = "BNDES"
        //            };
        //            session.Store(parceiroBndes);
        //            session.SaveChanges();
        //        }
        //    }
        //}
    }
}
