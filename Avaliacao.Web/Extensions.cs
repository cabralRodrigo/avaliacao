using Avaliacao.Aplicacao.Repositorio;
using Avaliacao.Dados;
using Avaliacao.Dominio.Repositorio;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace Avaliacao.Web
{
    public static class Extensions
    {
        public static void AddNHibernate(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<ISessionFactory>(DadosConfigurador.CriarSessionFactory(connectionString));
            services.AddScoped<ISession>(provider => provider.GetRequiredService<ISessionFactory>().OpenSession());
        }

        public static void AddRepositorios(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IClienteTelefoneRepositorio, ClienteTelefoneRepositorio>();
        }
    }
}