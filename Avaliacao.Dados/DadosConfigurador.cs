using Avaliacao.Dados.Mapping;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Avaliacao.Dados
{
    public static class DadosConfigurador
    {
        public static ISessionFactory CriarSessionFactory(string connectionString)
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<ClienteMap>();
            mapper.AddMapping<ClienteTelefoneMap>();

            var configuration = new Configuration();
            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            configuration.DataBaseIntegration(db =>
            {
                db.ConnectionString = connectionString;
                db.Driver<SqlClientDriver>();
                db.Dialect<MsSql2012Dialect>();
            });

            return configuration.BuildSessionFactory();
        }
    }
}
