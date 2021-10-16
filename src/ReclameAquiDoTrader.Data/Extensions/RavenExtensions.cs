using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using ReclameAquiDoTrader.Data.Indexes;
using System.Linq;

namespace ReclameAquiDoTrader.Data.Extensions
{
    public static class RavenExtensions
    {
        public static void Inicializar(this IDocumentStore store)
        {
            EnsureExists(store);
            store.OnAfterSaveChanges += Teste;
            IndexesSetup.Execute(store);
        }
        private static void Teste(object sender, AfterSaveChangesEventArgs args)
        {

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
